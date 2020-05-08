using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVirus : MonoBehaviour
{

    public GameObject SlashAnim;
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
                    var ani = Instantiate(SlashAnim, rig.position, Quaternion.identity) as GameObject;
                    Destroy(rig.gameObject, .5F);
                    Destroy(ani.gameObject, .5F);
                    ScoreScript.scoreValue += 1;
                }
            }
        }
        // Slice Animation
        // Vector3 fingerPos = Input.GetTouch(0).position;
        // fingerPos.z = 0;
        // // Vector3 objPos = Camera.main.ScreenToWorldPoint(fingerPos);
        // var ani = Instantiate(SlashAnim, fingerPos, Quaternion.identity) as GameObject;
        // Destroy(ani, 1);
        // var cam = MainCamera.GetComponent<Camera>();

        // Vector3 point = new Vector3();
        // Event currentEvent = Event.current;
        // Vector2 mousePos = new Vector2();

        // // Get the mouse position from Event.
        // // Note that the y position from Event is inverted.
        // mousePos.x = currentEvent.mousePosition.x;
        // mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        // point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        // Instantiate(SlashAnim, point, Quaternion.identity);


        // End of Slice Animation
    }
}
