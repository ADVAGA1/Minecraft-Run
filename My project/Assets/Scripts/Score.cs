using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    [SerializeField]
    private int highscore;
    [SerializeField]
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }

    public void UpdateScore(int score)
    {
        this.score = score;

        if(highscore < score) highscore = score;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighscore()
    {
        return highscore;
    }

}
