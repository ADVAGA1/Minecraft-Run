using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchoScript : MonoBehaviour
{
    private float timer1, timer2;
    private bool startTimer, pinchado, movido;
    // Start is called before the first frame update
    void Start()
    {
        timer1 = 0.1f;
        timer2 = 0.75f;
        startTimer = false; pinchado = false; movido = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer1 -= Time.deltaTime;
            timer2 -= Time.deltaTime;
        }

        if (!pinchado && timer1 <= 0)
        {
            transform.Translate(0, 0.2f, 0);
            pinchado = true;
        }

        if (!movido && timer2 <= 0)
        {
            transform.Translate(0, -0.2f, 0);
            movido = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool godMode = FindObjectOfType<PlayerMovement>().godMode;

        if(!godMode && collision.collider.name == "Player")
        {
            startTimer = true;
            FindObjectOfType<AudioManager>().Play("button");
        }
    }
}
