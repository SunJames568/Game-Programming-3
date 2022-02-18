using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookTrigger : MonoBehaviour
{
    // volume, audio, and audio source
    public float vol = 0.5f;
    public AudioClip spookSound;
    public AudioSource audioSource;

    // used to check if trigger has already been used
    private bool used = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !used) {
            audioSource.PlayOneShot(spookSound, vol);
            used = true;
        }
    }
}
