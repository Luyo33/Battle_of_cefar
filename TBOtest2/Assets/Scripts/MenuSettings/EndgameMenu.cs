using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameMenu : MonoBehaviour
{
    public GameObject WinButton;
    public GameObject LoseButton;
    private static bool win = false;

    public void SetWin(bool w)
    {
        win = w;
    }

    void Start()
    {
        if (WinButton != null)
        {
            if (win)
            {
                WinButton.SetActive(true);
                LoseButton.SetActive(false);
            }
            else
            {
                WinButton.SetActive(true);
                LoseButton.SetActive(true);
            }
        }
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
        DestroyImmediate(FindObjectOfType<AudioManager>().gameObject);
    }
    
}
