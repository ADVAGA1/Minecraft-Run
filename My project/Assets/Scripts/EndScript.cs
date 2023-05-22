using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public GameObject inGameCanvas;
    public GameObject outGameCanvas;

    public void EndGame()
    {
        Score score = FindObjectOfType<Score>();
        outGameCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = score.GetScore().ToString();
        outGameCanvas.transform.GetChild(5).GetComponent<TMP_Text>().text = score.GetHighscore().ToString();

        inGameCanvas.SetActive(false);
        outGameCanvas.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
