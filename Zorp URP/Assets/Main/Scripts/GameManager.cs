using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaveMaster LevelWaves;
    public GameObject ZorpSpawn;
    public int WaveNumber;
    public int WaveSegement;
    public float time;
    public float spawnInterval;

    public GameObject[] ZorpIndex;

    public List<GameObject> ZorpSpawnList;
    public GameObject[] ZorpsSpawnQueue;
    public int NextZorpSpawnInt;
    public GameObject currentZorp;
    

    // Start is called before the first frame update
    void Start()
    {
        StartWaves();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartWaves();
        }

        time = Time.deltaTime;
    }
    

    void StartWaves()
    {
        int Waves = LevelWaves.LevelWaves.Length;

        Debug.Log(Waves);


        //Disect Wave Object
        for (int i = 0; i < Waves; i++)
        {
            int WaveSegments = LevelWaves.LevelWaves[WaveNumber].WaveSegments.Length;
            Debug.Log("Itt: " + i + "Segs:" + WaveSegments);

            for (int z = 0; z < WaveSegments; z++)
            {
                for (int n = 0; n < LevelWaves.LevelWaves[WaveNumber].WaveSegments.Length; n++)
                {
                    int EnemyID = LevelWaves.LevelWaves[WaveNumber].WaveSegments[z].EnemyTypeID;
                    int EnemyAmount = LevelWaves.LevelWaves[WaveNumber].WaveSegments[z].AmountOfEnemies;
                    CompileWave(EnemyID, EnemyAmount);
                }

            }
        }

        GameObject[] _ZorpsSpawnQueue = ZorpSpawnList.ToArray();

        ZorpsSpawnQueue = _ZorpsSpawnQueue;

        NextZorpSpawnInt = 0;
        StartCoroutine("Spawn");
    }

    void CompileWave(int EnemyID, int EnemyAmount)
    {
        ZorpSpawnList.Add(ZorpIndex[1]);
    }

    IEnumerator Spawn()
    {
        if(ZorpsSpawnQueue.Length > NextZorpSpawnInt)
        {
            Debug.Log("Spawn");
            //Instantiate(ZorpsSpawnQueue[NextZorpSpawnInt], ZorpSpawn.transform.position);

            yield return new WaitForSeconds(2);

            StartCoroutine("Spawn");
        }




    }

}
