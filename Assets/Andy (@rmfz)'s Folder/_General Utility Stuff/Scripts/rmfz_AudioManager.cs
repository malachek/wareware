using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//This script is written by Andy (@rmfz)
//Please contact me if you make any changes, or need to discuss something in this script!

//lmao i had to rewrite this entire thing cuz of one overlook :sadge:

public class rmfz_AudioManager : MonoBehaviour
{
    public rmfz_Audio[] audios;
    private Dictionary<string, Coroutine> audioCoroutines = new Dictionary<string, Coroutine>();

    private void Awake()
    {
        foreach(rmfz_Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.introSequence == null ? audio.clip : audio.introSequence;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loopTrack;
        }
    }

    /// <summary>
    /// Plays an audio once.
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayAudio(string audioName)
    {
        rmfz_Audio audio = Array.Find(audios, audio => audio.audioName == audioName);
        if(audio == null)
        {
            Debug.LogError("[rmfz_AudioManager.cs] audioName not found!");
            return;
        }
        if(audio.introSequence != null)
        {
            StartCoroutine(PlayWithIntro(audio));
        }
        else
        {
            audio.source.Play();
        }
    }

    //compliments PlayAudio()
    IEnumerator PlayWithIntro(rmfz_Audio audio)
    {
        audio.source.Play();
        yield return new WaitForSeconds(audio.source.clip.length);
        audio.source.clip = audio.clip;
        audio.source.loop = true;
        audio.source.Play();
    }


    /// <summary>
    /// Plays an audio on loop. Optionally, you can specify a number of times to loop. If not specified, will loop indefinitely till stopped.
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayAudioWithLoop(string audioName)
    {
        rmfz_Audio audio = Array.Find(audios, audio => audio.audioName == audioName);
        if(audio != null)
        {
            audio.source.loop = true;
            audio.source.Play();
        }
        else
        {
            Debug.LogError("[rmfz_AudioManager.cs] audioName not found!");
            return;
        }
    }
    /// <summary>
    /// Plays an audio on loop. Optionally, you can specify a number of times to loop. If not specified, will loop indefinitely till stopped.
    /// </summary>
    /// <param name="audioName"></param>
    /// <param name="timesToLoop">Fill out this parameter if you want the audio to loop. If left blank, the audio will loop indefinitely till stopped.</param>
    public void PlayAudioWithLoop(string audioName, int timesToLoop)
    {
        rmfz_Audio audio = Array.Find(audios, audio => audio.audioName == audioName);
        if (audio != null && timesToLoop >= 0)
        {
            if (audioCoroutines.ContainsKey(audioName))
            {
                StopCoroutine(audioCoroutines[audioName]);
                audioCoroutines.Remove(audioName);
            }

            Coroutine coroutine = StartCoroutine(PlayAudioLoopCoroutine(audio, timesToLoop));
            audioCoroutines[audioName] = coroutine;
        }
        else
        {
            if (timesToLoop < 0)
            {
                Debug.LogError("[rmfz_AudioManager.cs] bro why u specify a negative number of times to loop wtf man");
                return;
            }
            else
            {
                Debug.LogError("[rmfz_AudioManager.cs] ur audio is somehow null so the code can't play it");
                return;
            }
        }
    }
    IEnumerator PlayAudioLoopCoroutine(rmfz_Audio audio, int timesToLoop)
    {
        audio.source.loop = false;
        for (int i = 0; i < timesToLoop; i++)
        {
            audio.source.Play();
            yield return new WaitForSeconds(audio.source.clip.length);
        }
    }
    
    /// <summary>
    /// Stops a specifiable audio from playing.
    /// </summary>
    /// <param name="audioName"></param>
    public void StopAudio(string audioName)
    {
        rmfz_Audio audio = Array.Find(audios, audio => audio.audioName == audioName);
        if (audio == null)
        {
            Debug.Log("[rmfz_AudioManager.cs] ur audio is somehow null so the code can't stop it");
            return;
        }
        else
        {
            if (audioCoroutines.TryGetValue(audioName, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                audioCoroutines.Remove(audioName); //remove from dictionary
            }
            audio.source.Stop();
        }
    }

    public void StopAllAudio()
    {
        StopAllCoroutines();
        foreach(rmfz_Audio audio in audios)
        {
            audio.source.Stop();
        }
    }
}

[System.Serializable]
public class rmfz_Audio
{
    public string audioName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loopTrack = false;

    [HideInInspector]
    public AudioSource source;

    public int timesToLoop = 0;
    public AudioClip introSequence; //it's what plays before "clip" plays and loops forever until stopped. there's no need to have anything in here.
}
