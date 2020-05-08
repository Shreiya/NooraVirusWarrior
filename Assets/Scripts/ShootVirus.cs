using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVirus : MonoBehaviour
{

    public GameObject SlashAnim;
    public GameObject MainCamera;
    // public AudioSource OnCoronaDestroy;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var rig = hitInfo.collider.GetComponent<Rigidbody>();
                if (rig != null)
                {
                    // Play slice Animation
                    var ani = Instantiate(SlashAnim, rig.position, Quaternion.identity) as GameObject;

                    // Destroy Gameobjects
                    Destroy(rig.gameObject, .5F);
                    Destroy(ani.gameObject, .5F);
                    if(PlayerPrefs.GetInt("SOUND") == 1)
                    {
                        rig.GetComponent<AudioSource>().Play();
                    }

                    // Update Score
                    ScoreScript.scoreValue += 1;
                }
            }
        }
    }
}
