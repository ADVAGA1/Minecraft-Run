using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamanteScript : MonoBehaviour
{
    public float rotationSpeed;
    public float animationSpeed;
    public float animationAmplitude;
    private float timer;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (gameObject != null)
        {
            transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
            transform.position = initialPosition + Vector3.up * oscillate(timer, animationSpeed, animationAmplitude);

        }
    }
    float oscillate(float time, float speed, float amplitude)
    {
        return Mathf.Cos(time * speed/Mathf.PI) * amplitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
