using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//This script is written by Andy (@rmfz)
//Please contact me if you make any changes, or need to discuss something in this script!

//lmao funny 69 overload function
public class rmfz_AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();

    /// <summary>
    /// Plays an audio once.
    /// </summary>
    /// <param name="audioClipListIndex"></param>
    public void PlayAudio(int audioClipListIndex)
    {
        audioSource.clip = (audioClipListIndex < audioClips.Count) ? audioClips[audioClipListIndex] : null;
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("[rmfz_AudioManager] Audio clip index input out of range!");
        }
    }

    /// <summary>
    /// Plays an audio on loop until explicitly told to stop.   
    /// </summary>
    /// <param name="audioClipListIndex"></param>
    /// <param name="loopStart"></param>
    /// <param name="loopEnd"></param>
    public void PlayAudio(int audioClipListIndex, float loopStart, float loopEnd)
    {
        audioSource.clip = (audioClipListIndex < audioClips.Count) ? audioClips[audioClipListIndex] : null;
        if (audioSource.clip != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("[rmfz_AudioManager] Audio clip index input out of range!");
        }
    }

    /// <summary>
    /// Plays an audio on loop, looping a certain number of times before stopping.
    /// </summary>
    /// <param name="loopStart"></param>
    /// <param name="loopEnd"></param>
    /// <param name="timesToLoop"></param>
    /// <param name="audioClipListIndex"></param>
    public void PlayAudio(float loopStart, float loopEnd, int timesToLoop, int audioClipListIndex)
    {
        audioSource.clip = (audioClipListIndex < audioClips.Count) ? audioClips[audioClipListIndex] : null;
        if (audioSource.clip != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("[rmfz_AudioManager] Audio clip index input out of range!");
        }
    }

    /// <summary>
    /// Plays an audio on loop, but you can specify an "intro", aka a portion of an audio that plays once, and another portion of the audio that plays on loop until stopped.
    /// </summary>
    /// <param name="audioClipListIndex"></param>
    /// <param name="introStart"></param>
    /// <param name="introStop"></param>
    /// <param name="loopStart"></param>
    /// <param name="loopStop"></param>
    public void PlayAudio(int audioClipListIndex, float introStart, float introStop, float loopStart, float loopStop)
    {
        audioSource.clip = (audioClipListIndex < audioClips.Count) ? audioClips[audioClipListIndex] : null;
        if (audioSource.clip != null)
        {
            audioSource.time = introStart;
            audioSource.Play();
            float introDuration = audioSource.clip.length - introStart;
            StartCoroutine(PlayLoopAfterIntroCoroutine(introDuration, loopStart, loopStop));
        }
        else
        {
            Debug.LogError("[rmfz_AudioManager] Audio clip index input out of range!");
        }
    }

    IEnumerator PlayLoopAfterIntroCoroutine(float introDuration, float loopStart, float loopEnd)
    {
        yield return new WaitForSeconds(introDuration);

        audioSource.loop = true;
        audioSource.time = loopStart;
        audioSource.Play();
    }

    private void Update()
    {
        
    }
}
