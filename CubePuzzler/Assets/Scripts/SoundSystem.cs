using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hover;
    public AudioClip click;
    public AudioClip back;

    // Simple enough. Most happens in Event Trigger and menu actions themselves.
    public void Hover()
    {
        audioSource.PlayOneShot(hover);
    }
    
    public void Click()
    {
        audioSource.PlayOneShot(click);
    }

    public void Back()
    {
        audioSource.PlayOneShot(back);
    }
}
