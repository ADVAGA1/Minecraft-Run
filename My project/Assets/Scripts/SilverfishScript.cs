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
    private float timer;
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

    }

    float oscillate(float time, float speed, float amplitude)
    {
        return Mathf.Cos(time * speed / Mathf.PI) * amplitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            
        }
    }
}
