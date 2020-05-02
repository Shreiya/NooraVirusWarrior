using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * 1.5f);
        transform.Translate(Vector3.left * Time.deltaTime * 0.1f);
    }
}
