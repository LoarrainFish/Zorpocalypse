using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveMaster _WaveMaster;
    public Wave[] Waves;
    public WaveSegment[] CurrentWaveSegements;
    public GameObject ZorpSpawnFocus;
    public int WaveNumber;
    public int WaveSegment;
    public float time; 



    // Start is called before the first frame update
    void Start()
    {
        GenerateWaves();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWaves()
    {
        foreach(Wave wave in _WaveMaster.LevelWaves)
        {

        }

    }
}
