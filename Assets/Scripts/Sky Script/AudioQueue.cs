using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    
    public AudioClip joeSodaAudio;
    public AudioClip clapAudio;

    public AudioSource joeSnoring;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void ClapSound()
    {
        audioSource.PlayOneShot(clapAudio);
    }

    public void JoeSound()
    {
        joeSnoring.mute = true;
        audioSource.PlayOneShot(joeSodaAudio);
    }
}
