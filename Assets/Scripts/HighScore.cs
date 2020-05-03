using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static int endValue = 0;
    Text HighScoree;

    // Start is called before the first frame update
    void Start()
    {
        HighScoree = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        HighScoree.text = "" + ScoreScript.scoreValue;
    }
}
