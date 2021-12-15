using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ChimeObject : MonoBehaviour
{
    [SerializeField]
    ChimeObjectListSO chimesList;
    [SerializeField]
    public int note = 64;
    [SerializeField]
    public float duration;
    [SerializeField]
    UdpSocket coms;
    [SerializeField]
    VisualEffect visualEffect;

    public bool notePlaying { get; private set; }
    private int _id;
    public int ID => _id;


    // Start is called before the first frame update
    void OnEnable()
    {
        if (chimesList != null)
            chimesList.chimes.Add(this);
        _id = GetInstanceID();
        coms = FindObjectOfType<UdpSocket>();
    }

    void OnDisable()
    {
        if (chimesList != null)
            chimesList.chimes.Remove(this);

    }

    public void Trigger()
    {
        notePlaying = true;
        // Debug.Log($"{name} got triggered");
        visualEffect.Play();
        coms.SendData($"NoteOn {note}");
        StartCoroutine(NoteOffAfterDelay(duration));
    }

    private IEnumerator NoteOffAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        coms.SendData($"NoteOff {note}");
        notePlaying = false;
        // Debug.Log($"{name} turning off");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
