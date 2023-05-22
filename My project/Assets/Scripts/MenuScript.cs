using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayClick()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Juego");
    }

    public void HowToPlayClick()
    {
        Debug.Log("How to play");
    }

    public void CreditsClick()
    {
        Debug.Log("Credits");
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
