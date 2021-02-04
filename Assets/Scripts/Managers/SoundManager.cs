using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource Ambience;
    public AudioClip ambience1;
    public AudioClip ambience2;
    public AudioClip ambience3;

    public AudioMixerSnapshot Menu;
    public AudioMixerSnapshot Level;
    public AudioMixerSnapshot MuteMusic;
    public AudioMixerSnapshot NoSFX;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (!Ambience.isPlaying)
        {
            int var = Random.Range(0,3);
            switch(var)
            {
                case 0:
                    Ambience.PlayOneShot(ambience1);
                    break;
                case 1:
                    Ambience.PlayOneShot(ambience2);
                    break;
                case 2:
                    Ambience.PlayOneShot(ambience3);
                    break;
                
            }
        }
    }
}
