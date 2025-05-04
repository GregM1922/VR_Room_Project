using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipInteract : MonoBehaviour
{
    public AudioSource interactNoise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactor") {
            {
                interactNoise.Play();
            } 
        }
    }
}
