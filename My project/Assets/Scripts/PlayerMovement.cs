using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

enum Moviment
{
    FORWARD, LEFT
}

public class PlayerMovement : MonoBehaviour
{
    public float velocity;
    public float jumpingForce;
    public Rigidbody myRigidbody;
    private bool left, changed;
    private int jumpCounter;
    private Moviment currentMov;

    private float offsetx, offsetz;

    // Start is called before the first frame update
    void Start()
    {
        left = false;
        changed = false;
        jumpCounter = 0;
        offsetx = 0; offsetz = 0;
        currentMov = Moviment.FORWARD;
    }

    // Update is called once per frame
    void Update()
    {

        bool ray = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);

        if (ray)
        {
            offsetx = hitInfo.collider.transform.position.x - transform.position.x;
            offsetz = hitInfo.collider.transform.position.z - transform.position.z;
        }
        else
        {
            offsetx = 0; offsetz=0;
        }


        if (ray && !changed && hitInfo.collider.name == "Change")
        {
            if (jumpCounter < 1 && Input.GetKeyDown(KeyCode.Space))
            {   
                left = !left;
                changed = true;
            }
        }
        else
        {
            if (jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = Vector3.up * jumpingForce;
                ++jumpCounter;
            }
        }

        if (left)
        {
            if (currentMov != Moviment.LEFT)
            {
                currentMov = Moviment.LEFT;
                transform.Rotate(0, 90, 0);
            }
            transform.position += new Vector3(Time.deltaTime, 0, offsetz / 500.0f) * velocity;
        }
        else
        {
            if (currentMov != Moviment.FORWARD)
            {
                currentMov = Moviment.FORWARD;
                transform.Rotate(0, -90, 0);
            }
            transform.position += new Vector3(offsetx / 500.0f, 0, Time.deltaTime) * velocity;
        }

        if (ray && hitInfo.collider.name != "Change") changed = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCounter = 0;
    }
}
