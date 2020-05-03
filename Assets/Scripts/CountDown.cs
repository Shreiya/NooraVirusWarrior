using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float count;
    public Text countText;
    public GameObject countDownn;
    public GameObject CountText;
    public GameObject Timerr;
    public GameObject HighScore;


    private void Start()
    {
        Timerr.SetActive(false);
        HighScore.SetActive(false);
    }

    private void Update()
    {
        count -= Time.deltaTime;
        WriteToText(count.ToString("F0"));


        if (count <= 0)
        {
            countDownn.SetActive(false);
            CountText.SetActive(false);
            Timerr.SetActive(true);

        }

    }

    void WriteToText(string strr)
    {
        countText.text = (strr);
    }
}
