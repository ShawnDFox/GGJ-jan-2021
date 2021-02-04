using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSFX : MonoBehaviour
{
    private BotEngine eng;
    private AudioSource BotSounds;
    public AudioClip Recojer1;
    public AudioClip Recojer2;
    public AudioClip Bot1;
    public AudioClip bot2;

    private void Awake()
    {
        BotSounds = GetComponent<AudioSource>();
        eng = GetComponent<BotEngine>();
        GetComponent<BotInventory>().OnItemPickup += PickUp;
    }

    private void PickUp(string arg1, int arg2, int arg3)
    {
        var recojer = Recojer1;
        if((int)UnityEngine.Random.Range(0, 1) == 0)
        {
            recojer = Recojer1;
        }
        else {
            recojer = Recojer2;
        }
        BotSounds.PlayOneShot(recojer);
    }

    public void moveSounds()
    {
        var engine=Bot1;
        if (!BotSounds.isPlaying)
        {
            if ((int)UnityEngine.Random.Range(0, 2) == 0)
            {
                engine = Bot1;
            }
            else
            {
                engine = bot2;
            }
            BotSounds.PlayOneShot(engine);
        }
    }
}
