using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscendentMusic : MonoBehaviour
{
    private float currentMusicTime;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        currentMusicTime = gameObject.GetComponent<AudioSource>().time;
    }

    void OnLevelWasLoaded(int lvl)
    {
        gameObject.GetComponent<AudioSource>().time = currentMusicTime;
    }
}
