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
    public Image TimerIcon;
    public Image ZorpIcon;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI ZorpsRemainingText;

    public static int ZorpsAlive;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWaves();
        UnpackWave();
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
        SpawnZorps(1);
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
                StartCoroutine(WaveDelay(60));
                UpdateStatusBar(WaveNumber, false);
                break;
            //Normal Wave Spawning State
            case 2:
                if (WaveNumber == 0)
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

                    if (ZorpsAlive == 0)
                    {
                        SpawnZorps(4);
                    }
                    else 
                    {
                        StartCoroutine(CheckForRoundEnd());
                    }
                }
                if (UIUpdate)
                {
                    Debug.Log("Update UI");

                    UpdateStatusBar(WaveNumber, false);
                    UIUpdate = false;
                }
                break;
            //Wave Downtime
            case 3:
                UpdateStatusBar(WaveNumber, true);
                CountDownClock(20f);
                StartCoroutine(WaveDelay(20));
                break;
            //After all Zorps are dead
            case 4:
                WaveNumber++;
                spawnNumberWave = 0;
                Debug.Log("Wave Ended");
                UIUpdate = false;
                SpawnZorps(3);
                break;
        }
    }
    public void UpdateStatusBar(int waveNumber, bool postWavePreperation)
    {
        if (!postWavePreperation)
        {
            if (waveNumber != 0)
            {
                TimerIcon.gameObject.SetActive(false);
                ZorpIcon.gameObject.SetActive(true);
                ZorpsRemainingText.gameObject.SetActive(true);
                TimerText.gameObject.SetActive(false);
                statusText.text = "WAVE " + waveNumber;
            }
            else
            {
                TimerIcon.gameObject.SetActive(true);
                ZorpIcon.gameObject.SetActive(false);
                TimerText.gameObject.SetActive(true);
                ZorpsRemainingText.gameObject.SetActive(false);
                statusText.text = "PREPERATION!";
            }
        }
        else
        {
            TimerIcon.gameObject.SetActive(true);
            ZorpIcon.gameObject.SetActive(false);
            TimerText.gameObject.SetActive(true);
            ZorpsRemainingText.gameObject.SetActive(false);
            statusText.text = "NEXT WAVE IN: ";
        }


    }

    public void EnemiesRemaining(int UpdateAmount)
    {
        Debug.Log("ENEMIES");
        ZorpsAlive = ZorpsAlive + UpdateAmount;
        ZorpsRemainingText.text = ZorpsAlive.ToString();
    }


    public void CountDownClock(float TimeToCountDown)
    {
        Debug.Log("Counting Down: " + TimeToCountDown);
        Timer.Countdown(TimeToCountDown);     
    }

    public IEnumerator CheckForRoundEnd()
    {
        yield return new WaitForSeconds(1);
        SpawnZorps(2);
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
