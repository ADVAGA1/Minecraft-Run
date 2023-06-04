using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void PlayAtPoint(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        AudioSource.PlayClipAtPoint(s.clip, position);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        s.source.Stop();
    }

    public bool isPlaying()
    {
        foreach (var sound in sounds)
        {
            if(sound.source.isPlaying) return true;
        }
        return false;
    }
}
