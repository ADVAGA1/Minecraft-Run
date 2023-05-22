using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ArrowScript : MonoBehaviour
{

    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(initialPosition.z - transform.position.z >= 3.2f*0.32f*7 || initialPosition.x - transform.position.x >= 3.2f*0.32f*7) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Player")
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        Destroy(gameObject);
    }
}
