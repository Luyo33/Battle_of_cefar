using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public InputField inputFieldText;

    private string username;
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

    public void SetUsername()
    {
        Debug.Log(inputFieldText.text);
        username = inputFieldText.text;
        Debug.Log(username);
    }
    
    public string GetUsername()
    {
        return username;
    }
    
}
