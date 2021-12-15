using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimePlayerSphere : MonoBehaviour
{
    [SerializeField]
    ChimeObjectListSO chimesList;
    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float errorCorrectionOffset = 0.01f;

    private Dictionary<int, bool> chimeSides;
    private Dictionary<int, float> chimeDistanceFromPlane;

    private ChimeObject lastChime = null;
    private ChimeObject currentChime = null;

    // Start is called before the first frame update
    void Start()
    {
        chimeSides = new Dictionary<int, bool>();
        chimeDistanceFromPlane = new Dictionary<int, float>();
        UpdateSidesAndCheckForChanges();
    }

    private ChimeObject UpdateSidesAndCheckForChanges()
    {
        ChimeObject ret = null;

        foreach (ChimeObject co in chimesList.chimes)
        {
            bool oldside;
            if (!chimeSides.TryGetValue(co.ID, out oldside))
            {
                oldside = GetDistanceFromPlane(co) > 0;
                chimeSides[co.ID] = oldside;
            }
            float olddist;
            if (!chimeDistanceFromPlane.TryGetValue(co.ID, out olddist))
            {
                olddist = GetDistanceFromPlane(co);
                chimeDistanceFromPlane[co.ID] = olddist;
            }


            float newDist = GetDistanceFromPlane(co);
            bool newside = oldside ? newDist > -errorCorrectionOffset : newDist > errorCorrectionOffset;
            if (newside != oldside)
            {
                chimeSides[co.ID] = newside;
                if (co != lastChime && co != currentChime)
                {
                    ret = co;
                }
                else
                {
                    Debug.Log("Was playing so not switching");
                }
            }
        }

        return ret;
    }

    private float GetDistanceFromPlane(ChimeObject chimeObject)
    {
        float dp = Vector3.Dot(transform.up, (chimeObject.transform.position - transform.position).normalized);
        return dp;
    }

    // Update is called once per frame
    void Update()
    {
        ChimeObject changed = UpdateSidesAndCheckForChanges();
        if (changed != null)
        {
            lastChime = currentChime;
            currentChime = changed;
            Debug.Log($"Switching to {changed}");
            changed.Trigger();
            transform.rotation = Quaternion.LookRotation(changed.transform.position - transform.position, transform.up);

            if (Random.Range(0f, 1f) > 0.5f)
                rotationSpeed *= -1;
        }

        transform.Rotate(Vector3.forward * rotationSpeed, Space.Self);
    }
}
