using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject[] Baloons;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(4);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(Baloons[i], SpawnPoint[i].position, Quaternion.identity);
        }
        StartCoroutine(StartSpawning());
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 1.5f);
    }
}