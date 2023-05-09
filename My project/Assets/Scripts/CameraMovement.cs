using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CameraMovement : MonoBehaviour
{
    public float velocity;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(Time.deltaTime, 0, Time.deltaTime) * velocity;
        
    }
}
