using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart_Button : MonoBehaviour
{
    private AudioSource restartButtonSound;

    void Start()
    {
        restartButtonSound = GetComponent<AudioSource>();
        if (restartButtonSound == null)
        {
            restartButtonSound = GetComponentInChildren<AudioSource>();
        }
    }

    public void PlayAudio()
    {
        if (restartButtonSound != null && !restartButtonSound.isPlaying)
        {
            restartButtonSound.Play();
        }
    }
}