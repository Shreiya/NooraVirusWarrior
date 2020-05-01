using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 1.0f);
        transform.Translate(Vector3.right * Time.deltaTime * 1.0f);
    }
}
