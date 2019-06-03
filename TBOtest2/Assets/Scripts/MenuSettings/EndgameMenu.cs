using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : MonoBehaviour
{
    public GameObject WinButton;
    public GameObject LoseButton;

    /*void Start()
    {
        if(win)
        {
            WinButton.SetActive(true);
            LoseButton.SetActive(false);
        }
        if(lose)
        {
            WinButton.SetActive(true);
            LoseButton.SetActive(true);
        }
    }*/

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
    
}
