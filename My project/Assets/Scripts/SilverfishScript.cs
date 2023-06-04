using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SilverfishScript : MonoBehaviour
{
    private Vector3 initialPosition;
    public float animationSpeed;
    public float animationAmplitude;
    private float timer, soundTimer;
    private bool forward;
    private float oscillateValue;
    private float lastOscillateValue;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        initialPosition = transform.position;
        forward = true;
        oscillateValue = oscillate(timer, animationSpeed, animationAmplitude);
        soundTimer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        lastOscillateValue = oscillateValue;

        oscillateValue = oscillate(timer, animationSpeed, animationAmplitude);

        if(forward && oscillateValue - lastOscillateValue > 0)
        {
            transform.Rotate(0, 180, 0);
            forward = !forward;
        }

        if(!forward && oscillateValue - lastOscillateValue < 0)
        {
            transform.Rotate(0, 180, 0);
            forward = !forward;
        }

        transform.position = initialPosition + (forward ? 1 : -1) * -1 *transform.forward * oscillateValue;

        if (soundTimer <= 0 && PlayerClose())
        {
            if (Random.value >= 0.7f)
            {
                FindObjectOfType<AudioManager>().PlayAtPoint("silverfish", transform.position);
                soundTimer = 5;
            }
            else soundTimer = 1;
        }

        soundTimer -= Time.deltaTime;

    }

    float oscillate(float time, float speed, float amplitude)
    {
        return Mathf.Cos(time * speed / Mathf.PI) * amplitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool godMode = FindObjectOfType<PlayerMovement>().godMode;

        if(!godMode && other.name == "Player")
        {
            FindObjectOfType<GameManager>().EndGame(Deaths.SILVERFISH);
        }
    }

    private bool PlayerClose()
    {
        Vector3 playerPos = FindObjectOfType<PlayerMovement>().transform.position;

        if (Vector3.Distance(playerPos, transform.position) <= 10) return true;
        return false;
    }

}
