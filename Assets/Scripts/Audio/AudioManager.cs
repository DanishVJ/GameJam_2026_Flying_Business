using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] MusicSounds, SFX;
    public AudioSource MusicSource, SFXSource;
    
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(MusicSounds, x => x.Name == name);
        if (s == null) {
            Debug.Log("Music Not Found");
        }

        else
        {
            MusicSource.clip = s.Clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(SFX, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("SFX Not Found");
        }

        else
        {
            SFXSource.PlayOneShot(s.Clip);
        }
    }
}
