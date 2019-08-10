using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{

    public AudioClip[] audioarray;
    public AudioSource audio;
    public AudioClipPlayer(AudioSource audio)
    {
        this.audio = audio;
        audioarray = new AudioClip[]
        {
             //Resources.Load("bark")   as AudioClip,
             //Resources.Load("meow")  as AudioClip
        };
    }

    public void PlaySound()
    {
        audio.PlayOneShot(audioarray[1]);
    }
}