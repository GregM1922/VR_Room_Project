using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.spatialBlend = 1f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (audioSource && collisionSound)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }
}
