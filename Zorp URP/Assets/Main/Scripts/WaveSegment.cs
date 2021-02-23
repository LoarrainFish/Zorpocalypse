using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Main/Wave Segment")]
public class WaveSegment : ScriptableObject
{
    [Tooltip(" Basic Zorp: 1 \n Armour Zorp: 2")]
    public int EnemyTypeID;
    public int AmountOfEnemies;
}
