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

public class GameManager : MonoBehaviour
{
    private bool gameEnded;
    // Start is called before the first frame update
    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        
    }

    public void EndGame()
    {
        if (!gameEnded)
        {
            Debug.Log("Game Over!");
            FindObjectOfType<PlayerMovement>().EndGame();
            gameEnded = true;
        }
    }
}
