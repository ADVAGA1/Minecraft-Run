using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    private float x;
    public float speed;
    private bool left;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        left = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (x >= 500 && !left) left = true;

        if (x <= -500 && left) left = false;

        if(left) transform.Translate(Time.deltaTime * Vector3.left * speed);
        else transform.Translate(Time.deltaTime * Vector2.right * speed);

        x += Time.deltaTime * (left ? -1 : 1) * speed;
    }
}
