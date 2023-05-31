using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public TMP_Text comments;
    private int score;
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
        comments.alpha -= 0.001f;
        float alpha = comments.transform.GetChild(0).transform.GetComponent<CanvasRenderer>().GetAlpha();
        comments.transform.GetChild(0).transform.GetComponent<CanvasRenderer>().SetAlpha(alpha - 0.001f);
    }

    public void UpdateScore(int score)
    {
        this.score = score;

        if (PlayerPrefs.GetInt("highscore") < score) PlayerPrefs.SetInt("highscore",score);
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt("highscore");
    }

    public void SetComment(string text)
    {
        comments.text = text;
        comments.alpha = 1;
        comments.transform.GetChild(0).transform.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

}
