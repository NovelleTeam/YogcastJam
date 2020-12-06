using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public sound[] sounds;
    public float volume;

    private void Awake()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Update()
    {
        foreach (sound s in sounds)
        {
            if(s.volume != volume)
                s.volume = volume;
        }
    }

    public void Play(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public float GetLength(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        return s.source.clip.length;
    }
}