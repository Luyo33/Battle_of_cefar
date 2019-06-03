using UnityEngine.Audio;
using UnityEngine;
using System;
using Photon.Pun;

public class AudioManager : MonoBehaviourPun
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = 1f;
        }
    }

    [PunRPC]
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

}
