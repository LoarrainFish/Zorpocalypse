using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaveMaster _WaveMaster;
    public List<Wave> WaveList;
    public Wave[] Waves;
    List<int> zorpIndexList;
    public List<WaveSegment> currentWaveSegementsList;
    public WaveSegment[] currentWaveSegements;
    public GameObject[] Zorps;
    public GameObject[] SpawnQueue;
    [HideInInspector]
    public List<GameObject> SpawnQueueList;
    public GameObject ZorpSpawnFocus;
    public GameObject ZorpSpawnLocation;
    public List<int> EnemiesPerWaveList;
    public int[] EnemiesPerWave;
    public int EnemiesThisWave;
    public int WaveNumber = 1;
    public int WaveSegment;
    public float time;

    private int spawnNumber = 1;


    // Start is called before the first frame update
    void Start()
    {
        GenerateWaves();
        UnpackWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateWaves()
    {

        for (int i = 0; i < _WaveMaster.LevelWaves.Length; i++)
        {
            WaveList.Add(_WaveMaster.LevelWaves[i]);
        }

        Waves = WaveList.ToArray();

    }

    public void UnpackWave()
    {


        for (int i = 0; i < Waves.Length; i++)
        {
            for (int a = 0; a < Waves[i].WaveSegments.Length; a++)
            {
                currentWaveSegementsList.Add(Waves[i].WaveSegments[a]);
            }
        }

        currentWaveSegements = currentWaveSegementsList.ToArray();
        QueueWave();
    }

    public void QueueWave()
    {
        for (int i = 0; i < currentWaveSegements.Length; i++)
        {
            int tempZorpID = currentWaveSegements[i].EnemyTypeID;
            int tempAmountOfEnemies = currentWaveSegements[i].AmountOfEnemies;
            ReadSegementContainer(tempZorpID, tempAmountOfEnemies);
        }

        SpawnQueue = SpawnQueueList.ToArray();
        EnemiesPerWave = EnemiesPerWaveList.ToArray();
        SpawnZorps();

    }

    public void ReadSegementContainer(int ZorpID, int AmountOfEnemies)
    {
        Debug.Log("Segement Unpacking!");
        for (int i = 0; i < AmountOfEnemies; i++)
        {
            SpawnQueueList.Add(Zorps[ZorpID]);
            EnemiesPerWaveList.Add(AmountOfEnemies);
        }

    }
    private void SpawnZorps()
    {
        if(spawnNumber <= SpawnQueue.Length)
        {
            Instantiate(SpawnQueue[spawnNumber], ZorpSpawnLocation.transform.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            StartCoroutine("SpawnDelay");
        }
        else
        {
            Debug.Log("Spawn Complete");
        }

    }

    public IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(0);

    }

    public IEnumerator SpawnDelay()
    {
        spawnNumber++;
        yield return new WaitForSeconds(2);
        SpawnZorps();

    }
    

}
