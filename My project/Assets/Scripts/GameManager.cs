using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
