using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseUIScript : MonoBehaviour
{
    public GameObject inGameCanvas;
    public GameObject pauseCanvas;
    public void PauseGame()
    {
        Score score = FindObjectOfType<Score>();
        pauseCanvas.transform.GetChild(1).GetComponent<TMP_Text>().text = score.GetScore().ToString();
        pauseCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = score.GetHighscore().ToString();
        FindObjectOfType<GameManager>().PauseGame(true);

        inGameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        FindObjectOfType<GameManager>().PauseGame(false);
    }

}
