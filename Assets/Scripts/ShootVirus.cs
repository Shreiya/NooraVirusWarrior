using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVirus : MonoBehaviour
{
    public GameObject MainCamera;
    //public GameObject smoke;

    // public void Shoot()
    // {
    //     RaycastHit hit;
    //     if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit))
    //     {
    //         if (hit.transform.name == "CoronaVirus(Clone)" || hit.transform.name == "1CoronaVirus(Clone)" || hit.transform.name == "2CoronaVirus(Clone)")
    //         {
    //             ScoreScript.scoreValue += 1;
    //             Destroy(hit.transform.gameObject);
    //             //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
    //         }
    //     }
    // }
    
    void Update()
    {
        // RaycastHit hit;

        // for (var i = 0; i < Input.touchCount; ++i)
        // {
        //     if (Input.GetTouch(i).phase == TouchPhase.Began)
        //     {
        //         Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
        //         if (Physics.Raycast(ray, out hit))
        //         {
        //             if (tag == "Enemy")
        //             {
        //                 ScoreScript.scoreValue += 1;
        //                 Destroy(hit.collider.gameObject);
        //             }
        //         }
        //     }
        // }


        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var rig = hitInfo.collider.GetComponent<Rigidbody>();
                if (rig != null)
                {
                    Destroy(rig.gameObject);
                    ScoreScript.scoreValue += 1;
                }
            }
        }
    }
}
