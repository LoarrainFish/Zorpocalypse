using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    GameObject GAMEMANAGER_GameObject;
    GameManager _GameManager;

    public Image[] Rounds;

    public Sprite victoryIcon;
    public Sprite emptyIcon;

    public Sprite[] queueIconsFinal;

    // Start is called before the first frame update
    void Start()
    {

        GAMEMANAGER_GameObject = GameObject.FindWithTag("GameManager");
        _GameManager = GAMEMANAGER_GameObject.GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateQueue(Sprite[] _queueIcons)
    {
        Debug.Log(_queueIcons.Length);
        Sprite[] queueIcons = new Sprite[_queueIcons.Length];
        Array.Copy(_queueIcons, queueIcons, _queueIcons.Length);
        Sprite[] _queueIconsTemp = new Sprite[queueIcons.Length + 6];
        for (int i = 0; i < queueIcons.Length; i++)
        {
            _queueIconsTemp[i] = queueIcons[i];
        }

        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                _queueIconsTemp[i + queueIcons.Length] = victoryIcon;
            }
            else
            {
                _queueIconsTemp[i + queueIcons.Length] = emptyIcon;
            }

        }
        queueIconsFinal = new Sprite[_queueIconsTemp.Length];
        for (int i = 0; i < _queueIconsTemp.Length; i++)
        {
            queueIconsFinal[i] = _queueIconsTemp[i];
        }

        UpdateQueue(0);

    }

    public void UpdateQueue(int WaveNumber)
    {
        Debug.Log(WaveNumber + "Update");
        for (int i = 0; i < Rounds.Length; i++)
        {
            Rounds[i].sprite = queueIconsFinal[i + WaveNumber];
        }
    }
}
