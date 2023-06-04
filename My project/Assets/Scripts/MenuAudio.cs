using UnityEngine;
using System;

public class MenuAudio : MonoBehaviour
{
    AudioManager audioManager;
    public int nAudios;
    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        var r = new System.Random();
        int value = r.Next(1,nAudios+1);

        if (value == nAudios + 1) value = value - 1;

        audioManager.Play("Music" + value);

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioManager.isPlaying())
        {
            var r = new System.Random();
            int value = r.Next(1, nAudios+1);

            if (value == nAudios + 1) value = value - 1;

            audioManager.Play("Music" + value);
        }
    }
}
