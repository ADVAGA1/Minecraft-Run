using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
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

}
