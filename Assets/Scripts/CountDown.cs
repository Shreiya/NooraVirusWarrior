using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float CountdownTime;
    public Text CountdownText;
    public GameObject CountdownScript;
    public GameObject Countdown;
    public GameObject TimerScript;
    // public GameObject TimerBackground;
    public GameObject HighScore;
    // public GameObject CrossHair;

    private void Start()
    {
        TimerScript.SetActive(false);
        // CrossHair.SetActive(true);
        HighScore.SetActive(false);
        // TimerBackground.SetActive(false);
    }

    private void Update()
    {
        CountdownTime-= Time.deltaTime;
        UpdateCountdown(CountdownTime.ToString("F0"));

        if (CountdownTime<= 0)
        {
            CountdownScript.SetActive(false);
            Countdown.SetActive(false);
            TimerScript.SetActive(true);
            // CrossHair.SetActive(true);
            // TimerBackground.SetActive(true);
        }
    }

    void UpdateCountdown(string txt)
    {
        CountdownText.text = (txt);
    }
}
