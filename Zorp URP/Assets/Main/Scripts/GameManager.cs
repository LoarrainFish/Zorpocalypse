using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private List<float> RoundLengthList;
    public float[] RoundLength;
    [HideInInspector]
    public int WaveID;
    public int WaveNumber = 1;
    public int WaveSegment;
    public float time;

    private float spawnDelayTime = 1.5f;

    public int spawnNumberWave = 0;
    public int spawnNumberTotal = 0;

    public TextMeshProUGUI statusText;

    private bool UIUpdate;


    // Start is called before the first frame update
    void Start()
    {
        GenerateWaves();
        UnpackWave();
        SpawnZorps(1);
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate = true;
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
        EnemiesPerWave = EnemiesPerWaveList.ToArray();
    }

    public void ReadSegementContainer(int ZorpID, int AmountOfEnemies, int WaveSegements)
    {

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
                CountDownClock(5f);
                UpdateStatusBar(WaveNumber, false);
                break;
            //Normal Wave Spawning State
            case 2:
                if(WaveNumber == 0)
                {
                    WaveNumber = 1;
                }
                if (spawnNumberWave <= EnemiesPerWave[WaveNumber - 1])
                {
                    spawnNumberWave++;
                    spawnNumberTotal++;
                    StartCoroutine(SpawnDelay());
                }
                else
                {
                    WaveNumber++;
                    spawnNumberWave = 0;
                    Debug.Log("Wave Ended");
                    SpawnZorps(3);
                    UIUpdate = false;
                }
                if (UIUpdate)
                {
                    Debug.Log("Update UI");
                    //CountDownClock(RoundLength[WaveNumber]);
                    UpdateStatusBar(WaveNumber, false);
                    UIUpdate = false;
                }
                break;
            //Wave Downtime
            case 3:
                UpdateStatusBar(WaveNumber, true);
                CountDownClock(10f);
                StartCoroutine(WaveDelay(10));
                break;
        }


    }
    public void UpdateStatusBar(int waveNumber, bool postWavePreperation)
    {
        Debug.Log(postWavePreperation);
        if (!postWavePreperation)
        {
            if (waveNumber != 0)
            {
                statusText.text = "WAVE " + waveNumber;

            }
            else
            {
                statusText.text = "PREPERATION!";
            }
        }
        else
        {
            statusText.text = "NEXT WAVE IN: ";
        }

    }


    public void CountDownClock(float TimeToCountDown)
    {
        Timer.Countdown(TimeToCountDown);     
    }

    public IEnumerator WaveDelay(int TimeToWait)
    {
        Debug.Log("Waiting for next round: " + TimeToWait + " seconds.");
        yield return new WaitForSeconds(TimeToWait);
        SpawnZorps(2);
    }

    public IEnumerator SpawnDelay()
    {
        Instantiate(SpawnQueue[spawnNumberTotal], ZorpSpawnLocation.transform.position, Quaternion.Euler(new Vector3(0f, 90f, 0f)));
        yield return new WaitForSeconds(spawnDelayTime);
        SpawnZorps(2);
    }
    

}
