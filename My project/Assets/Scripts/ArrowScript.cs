using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(initialPosition.z - transform.position.z >= 15 || initialPosition.x - transform.position.x >= 15) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}
