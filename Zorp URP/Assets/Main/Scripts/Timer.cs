using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public static bool timerIsRunning;
    public static float _timeToCountDown;
    public static float timeRemaining;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        timerIsRunning = false;
        Countdown(30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if(_timeToCountDown > 0)
            {
                _timeToCountDown -= Time.deltaTime;
                DisplayTime(_timeToCountDown);
            }
            else
            {
                _timeToCountDown = 0;
                timeRemaining = 0;
                timerIsRunning = false;
            }

        }
    }
    public static void Countdown(float timeToCountDown)
    {
        if (timerIsRunning)
        {
            _timeToCountDown = 0;
        }
        else
        {
            _timeToCountDown = timeToCountDown;
            timerIsRunning = true;
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        timerText.text = timeToDisplay.ToString("N0");
    }
}
