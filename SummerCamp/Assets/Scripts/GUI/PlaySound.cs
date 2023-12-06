using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource hoverSound;

    private void Awake()
    {
        hoverSound = GetComponent<AudioSource>();
    }
    public void PlayHover()
    {
        hoverSound.Play();
    }
}
