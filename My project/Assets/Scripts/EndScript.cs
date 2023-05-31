using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public GameObject inGameCanvas;
    public GameObject outGameCanvas;
    public TMP_Text comments;

    private void Update()
    {
        comments.alpha -= 0.001f;
        float alpha = comments.transform.GetChild(0).transform.GetComponent<CanvasRenderer>().GetAlpha();
        comments.transform.GetChild(0).transform.GetComponent<CanvasRenderer>().SetAlpha(alpha - 0.001f);
    }
    public void EndGame(Deaths death)
    {
        Score score = FindObjectOfType<Score>();
        outGameCanvas.transform.GetChild(4).GetComponent<TMP_Text>().text = score.GetScore().ToString();
        outGameCanvas.transform.GetChild(6).GetComponent<TMP_Text>().text = score.GetHighscore().ToString();

        inGameCanvas.SetActive(false);
        outGameCanvas.SetActive(true);

        SetComment(death);

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

    private void SetComment(Deaths death)
    {
        string text = "";
        switch (death)
        {
            case Deaths.FALL:
                text = "Player hit the ground too hard";
                break;
            case Deaths.ZOMBIE:
                text = "Player was slain by Zombie";
                break;
            case Deaths.ARROW:
                text = "Player was shot";
                break;
            case Deaths.PINCHO:
                text = "Player was impaled by spikes";
                break;
            case Deaths.SILVERFISH:
                text = "Player was slain by Silverfish";
                break;
        }

        comments.text = text;
        comments.alpha = 1;
        comments.transform.GetChild(0).transform.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

}
