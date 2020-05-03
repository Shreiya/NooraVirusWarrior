using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timerTime;
    public Text timerText;
    public GameObject Timerr;
    public GameObject ScoreText;
    public GameObject SpawnScript;
    public GameObject ShootScript;
    public GameObject button;
    public GameObject HighScore;
    public GameObject Textt;
    public GameObject Imagee;
    Text HighScoree;
    
    
    private void Update()
    {
        timerTime -= Time.deltaTime;
        WriteToText(timerTime.ToString("F1"));

        if (timerTime <= 0)
        {
            Timerr.SetActive(false);
            ScoreText.SetActive(false);
            SpawnScript.SetActive(false);
            ShootScript.SetActive(false);
            button.SetActive(false);
            Textt.SetActive(false);
            Imagee.SetActive(false);
            HighScore.SetActive(true);
           
        }

    }

    void WriteToText(string str_)
    {
        timerText.text =("Time Left:" + str_ + "sec");

      
    }

}
