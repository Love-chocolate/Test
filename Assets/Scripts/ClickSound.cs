using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioSource clickSound;

    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    public void StartClickSound()
    {
        if(clickSound.enabled == true)
        {
            clickSound.Play();
        }
    }
}
