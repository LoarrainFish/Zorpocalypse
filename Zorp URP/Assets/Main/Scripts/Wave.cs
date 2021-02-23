using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Main/Wave")]
public class Wave : ScriptableObject
{
    public int WaveID;
    public WaveSegment[] WaveSegments;
}
