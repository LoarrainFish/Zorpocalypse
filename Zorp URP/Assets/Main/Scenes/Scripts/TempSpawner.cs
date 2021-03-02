using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSpawner : MonoBehaviour
{
    public GameObject[] Zorps;
    public Transform spawnPoint;

    public int WaveAmount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnThings");
        WaveAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnThings()
    {
        Instantiate(Zorps[Random.Range(0, 4)], spawnPoint.position, spawnPoint.rotation);
        yield return new WaitForSeconds(1);
        if (WaveAmount != 15)
        {
            StartCoroutine("SpawnThings");
        }
        else
        {
            yield break;
        }


    }
}
