using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerRotation : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotación en grados por segundo
    public float minYRotation = 0f; // Valor mínimo de rotación en el eje X
    public float maxYRotation = 360f; // Valor máximo de rotación en el eje X

    private float currentRotation = 0f; // Rotación actual del martillo

    // Update is called once per frame
    void Update()
    {
        // Calcula el ángulo de rotación basado en la velocidad y el tiempo
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Actualiza la rotación actual del martillo
        currentRotation += rotationAmount;


        // Aplica la rotación al martillo
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,currentRotation , transform.rotation.eulerAngles.z);
    }
}
