using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVirus : MonoBehaviour
{
    public GameObject MainCamera;
    //public GameObject smoke;

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit))
        {
            if (hit.transform.name == "CoronaVirus(Clone)" || hit.transform.name == "1CoronaVirus(Clone)" || hit.transform.name == "2CoronaVirus(Clone)")
            {
                ScoreScript.scoreValue += 1;
                Destroy(hit.transform.gameObject);
                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

    }
}
