using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento en unidades por segundo

    // Update is called once per frame
    void Update()
    {
        // Calcula el desplazamiento en el eje Z basado en la velocidad y el tiempo
        float displacement = speed * Time.deltaTime;

        // Calcula la nueva posición en el eje Z
        float newPositionZ = transform.position.z - displacement;

        // Mueve el objeto a la nueva posición manteniendo la rotación actual
        transform.position = new Vector3(transform.position.x, transform.position.y, newPositionZ);
    }
}
