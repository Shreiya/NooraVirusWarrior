using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject[] Virus;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(4);
        for (int i = 0; i < 15; i++)
        {
            Instantiate(Virus[i], SpawnPoint[i].position, Quaternion.identity);
        }
        StartCoroutine(StartSpawning());
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 0.1f);
    }
}