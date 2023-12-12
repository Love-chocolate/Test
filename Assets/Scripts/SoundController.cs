using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource clickSound;

    public void EnableOrMuteBackgroundMusic()
    {
        if (_backgroundMusic.isPlaying)
        {
            _backgroundMusic.Pause();
        }
        else
        {
            _backgroundMusic.Play();
        }

    }

    public void EnableOrMuteClickSound()
    {
        if (clickSound.enabled)
        {
            clickSound.enabled = false;
        }
        else
        {
            clickSound.enabled = true;
        }
    }
}
