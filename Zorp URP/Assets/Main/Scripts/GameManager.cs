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
    [HideInInspector]
    public int WaveID;
    public int WaveNumber = 1;
    public int WaveSegment;
    public float time;

    public int spawnNumberWave = 0;
    public int spawnNumberTotal = 0;


    // Start is called before the first frame update
    void Start()
    {
        GenerateWaves();
        UnpackWave();
        EnemiesPerWave = EnemiesPerWaveList.ToArray();
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
                EnemiesThisWave += Waves[i].WaveSegments[a].AmountOfEnemies;
                    
            }
            EnemiesPerWaveList.Add(EnemiesThisWave);
            EnemiesThisWave = 0;
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
            ReadSegementContainer(tempZorpID, tempAmountOfEnemies, currentWaveSegements.Length);
        }

        SpawnQueue = SpawnQueueList.ToArray();
        //EnemiesPerWave = EnemiesPerWaveList.ToArray();
        SpawnZorps(1);

    }

    int itt;
    public void ReadSegementContainer(int ZorpID, int AmountOfEnemies, int WaveSegements)
    {

        Debug.Log("Segement Unpacking!");
        for (int i = 0; i < AmountOfEnemies; i++)
        {
            SpawnQueueList.Add(Zorps[ZorpID]);
        }




    }
    private void SpawnZorps(int state)
    {
        switch (state)
        {

            //First Wave. Longer Cooldown
            case 1:
                StartCoroutine(WaveDelay(5));
                break;
            //Normal Wave Spawning State
            case 2:
                StartCoroutine(SpawnDelay());
                break;
            //Wave Downtime
            case 3:
                StartCoroutine(WaveDelay(10));
                break;
        }


    }


    public void CountDownClock(float TimeToCountDown)
    {

        //Visual Countdown
        //Tick Time Down
        


    }

    public IEnumerator WaveDelay(int TimeToWait)
    {
        Debug.Log("Waiting for next round: " + TimeToWait + " seconds.");
        yield return new WaitForSeconds(TimeToWait);
        SpawnZorps(2);
    }

    public IEnumerator SpawnDelay()
    {
        if(spawnNumberWave <= EnemiesPerWave[WaveNumber])
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(SpawnQueue[spawnNumberTotal], ZorpSpawnLocation.transform.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            spawnNumberWave++;
            spawnNumberTotal++;
            SpawnZorps(2);

        }
        else
        {
            spawnNumberWave = 0;
            Debug.Log("Wave Ended");
            SpawnZorps(3);
        }

    }
    

}
