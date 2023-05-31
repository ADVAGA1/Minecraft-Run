using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Constants
{
    public const float FALL_TIMER = 3f;
    public const float PINCHO_TIMER = 0.1f;
    public const float Speed = 2.5f;
    public const float Slowdown = 0.66f;
    public const float blockSize = 3.2f;
}

public enum Deaths
{
    FALL, PINCHO, ZOMBIE, ARROW, SILVERFISH
}

public class GameManager : MonoBehaviour
{
    private bool gameEnded;
    private bool gamePaused;
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
        gamePaused = false;
    }

    public void PauseGame(bool pause)
    {
        gamePaused = pause;

        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void EndGame(Deaths death)
    {
        if (!gameEnded)
        {
            Debug.Log("Game Over!");
            FindObjectOfType<PlayerMovement>().EndGame(death);
            gameEnded = true;
        }
    }
}
