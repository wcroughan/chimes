using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeSpawner : MonoBehaviour
{
    [SerializeField, Min(1)]
    int numChimes = 10;
    [SerializeField]
    NoteSet noteSet;
    [SerializeField]
    ChimeObjectListSO chimeList;
    [SerializeField]
    GameObject chimePrefab;
    [SerializeField]
    float spawnRange;
    [SerializeField]
    float spawnZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void SpawnChime()
    {
        Vector3 pos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), spawnZ);
        GameObject go = Instantiate(chimePrefab, pos, Quaternion.identity);
        ChimeObject co = go.GetComponent<ChimeObject>();
        co.note = noteSet.GetRandomNote();
    }

    private void RemoveChime()
    {
        foreach (ChimeObject co in chimeList.chimes)
        {

            Destroy(co.gameObject);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (chimeList.chimes.Count < numChimes)
        {
            SpawnChime();
        }

        while (chimeList.chimes.Count > numChimes)
        {
            RemoveChime();
        }
    }

}
