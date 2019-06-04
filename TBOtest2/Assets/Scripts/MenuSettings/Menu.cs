using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class Menu : MonoBehaviour
{
    
    public InputField inputFieldText;


    public void Start()
    {
        PhotonNetwork.Disconnect();
    }
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
        DestroyImmediate(FindObjectOfType<PhotonRoom>().gameObject);
        DestroyImmediate(FindObjectOfType<AudioManager>().gameObject);
        SceneManager.LoadScene(0);
    }

    public void SetUsername()
    {
        gameObject.GetComponent<NickNameHolder>().SetNickName(inputFieldText.text);
    }
    
}
