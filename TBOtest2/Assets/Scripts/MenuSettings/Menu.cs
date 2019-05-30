﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //Everything below is for the main menu
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }

    //From here it is used to return to the menu from the launcher
    public void ReturntoMain()
    {
        SceneManager.LoadScene(0);
    }
    
    //From here everything is for the options menu

    public AudioMixer audioMixer;
    private Resolution[] resolutions;
    public Dropdown resolutionDropDown;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].width == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
    Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
