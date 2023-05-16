using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public enum Moviment
{
    FORWARD, LEFT
}

public class PlayerMovement : MonoBehaviour
{
    public float velocity;
    public float jumpingForce;
    public Rigidbody myRigidbody;
    private Animator myAnimator;
    private bool left, changed;
    private int jumpCounter;
    private bool onFloor;
    public Moviment currentMov { get; private set; }

    private float offsetx, offsetz;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.GetChild(0).GetComponent<Animator>();
        left = false;
        changed = false;
        onFloor = true;
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

        if (ray)
        {
            
            string name = hitInfo.collider.name;

            if(!changed && name == "Change")
            {
                if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    left = !left;
                    changed = true;
                }
            }
            else
            {
                if (jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
                {
                    if (jumpCounter < 1) myRigidbody.velocity = Vector3.up * jumpingForce;
                    else myRigidbody.velocity = Vector3.up * jumpingForce *0.75f;
                    jumpCounter++;
                    myAnimator.SetInteger("jumpCounter", jumpCounter);
                    onFloor = false;
                }
            }

            if(onFloor && name == "pincho")
            {
                if (left) myRigidbody.velocity = new Vector3(0, 4, -4);
                else myRigidbody.velocity = new Vector3(-4, 4, 0);
            }

            if (onFloor && name == "fango")
            {
                velocity = 2.5f * 0.5f;
            }
            else velocity = 2.5f;


            /*
            if (!changed && hitInfo.collider.name == "Change")
            {
                velocity = 2.5f;
                if (jumpCounter < 1 && Input.GetKeyDown(KeyCode.Space))
                {
                    left = !left;
                    changed = true;
                }
            }
            else if (onFloor && hitInfo.collider.name == "pincho")
            {
                velocity = 2.5f;
                if (left) myRigidbody.velocity = new Vector3(0, 5, -2);
                else myRigidbody.velocity = new Vector3(-2, 5, 0);
            }
            else if(onFloor && hitInfo.collider.name == "fango")
            {
                velocity = 2.5f * 0.50f;
            }
            else
            {
                velocity = 2.5f;
                if (jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
                {
                    if (jumpCounter < 1) myRigidbody.velocity = Vector3.up * jumpingForce;
                    else myRigidbody.velocity = Vector3.up * jumpingForce / 2;
                    jumpCounter++;
                    myAnimator.SetInteger("jumpCounter", jumpCounter);
                    onFloor = false;
                }
            }
            */
        }

        if (left)
        {
            if (currentMov != Moviment.LEFT)
            {
                currentMov = Moviment.LEFT;
                transform.Rotate(0, 90, 0);
            }
            transform.position += new Vector3(Time.deltaTime, 0, offsetz / 100.0f) * velocity;
        }
        else
        {
            if (currentMov != Moviment.FORWARD)
            {
                currentMov = Moviment.FORWARD;
                transform.Rotate(0, -90, 0);
            }
            transform.position += new Vector3(offsetx / 100.0f, 0, Time.deltaTime) * velocity;
        }

        if (ray && hitInfo.collider.name != "Change") changed = false;

    }

    private void OnCollisionEnter(Collision collision)
    { 
        jumpCounter = 0;
        myAnimator.SetInteger("jumpCounter", jumpCounter);
        onFloor = true;
    }
}
