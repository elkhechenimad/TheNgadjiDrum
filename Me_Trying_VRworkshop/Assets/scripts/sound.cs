using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    GameObject drumStick;
    AudioSource drumhitSound;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        drumhitSound = GetComponent<AudioSource>();
     
        if (other.gameObject.name == "drumStick")
        {
            
            drumhitSound.Play();

        }
        Debug.Log(other.gameObject.name);
    }
   /* private void OnCollisionEnter(Collision collision)
    {
        drumhitSound = GetComponent<AudioSource>();

        if (collision.gameObject.name == "drumStick")
        {

            drumhitSound.Play();

        }
        Debug.Log(collision.gameObject.name);
    }*/

}
