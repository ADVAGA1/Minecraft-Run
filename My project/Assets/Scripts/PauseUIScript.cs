using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseUIScript : MonoBehaviour
{
    public GameObject inGameCanvas;
    public GameObject pauseCanvas;
    public void PauseGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Score score = FindObjectOfType<Score>();
        pauseCanvas.transform.GetChild(1).GetComponent<TMP_Text>().text = score.GetScore().ToString();
        pauseCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = score.GetHighscore().ToString();
        FindObjectOfType<GameManager>().PauseGame(true);

        inGameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("running");
    }

    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        pauseCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        FindObjectOfType<GameManager>().PauseGame(false);
        FindObjectOfType<AudioManager>().Play("running");
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Application.Quit();

    }

    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("button");
        FindObjectOfType<GameManager>().PauseGame(false);
        SceneManager.LoadScene("Menu");
    }

}
