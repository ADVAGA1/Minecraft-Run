using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    FORWARD, LEFT
}

public class PlayerMovement : MonoBehaviour
{
    public float velocity;
    public float jumpingForce;
    private Rigidbody myRigidbody;
    private Animator myAnimator;
    private bool left, changed;
    private int jumpCounter;
    private bool onFloor, startTimerPincho;
    private float timerPincho;
    public Movement currentMov { get; private set; }

    private float offsetx, offsetz;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.GetChild(0).GetComponent<Animator>();
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        left = false;
        changed = false;
        onFloor = true;
        startTimerPincho = false;
        timerPincho = 0.1f;
        jumpCounter = 0;
        offsetx = 0; offsetz = 0;
        currentMov = Movement.FORWARD;
    }

    // Update is called once per frame
    void Update()
    {

        if(startTimerPincho) timerPincho -= Time.deltaTime;

        if(startTimerPincho && timerPincho <= 0)
        {
            if (left) myRigidbody.velocity = new Vector3(0, 4, -4);
            else myRigidbody.velocity = new Vector3(-4, 4, 0);
            startTimerPincho = false;
        }

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
                    myAnimator.SetBool("onFloor", onFloor);

                }
            }

        }
        else
        {
            
            if (jumpCounter > 0 && Input.GetKeyDown(KeyCode.Space))
            {

                if (jumpCounter < 1) myRigidbody.velocity = Vector3.up * jumpingForce;
                else myRigidbody.velocity = Vector3.up * jumpingForce * 0.75f;

                jumpCounter++;
                myAnimator.SetInteger("jumpCounter", jumpCounter);
                onFloor = false;
                myAnimator.SetBool("onFloor", onFloor);
            }
            
        }

        if (left)
        {
            if (currentMov != Movement.LEFT)
            {
                currentMov = Movement.LEFT;
                transform.Rotate(0, 90, 0);
            }

            transform.position += new Vector3(Time.deltaTime, 0, offsetz / 200.0f) * velocity;
        }
        else
        {
            if (currentMov != Movement.FORWARD)
            {
                currentMov = Movement.FORWARD;
                transform.Rotate(0, -90, 0);
            }

            transform.position += new Vector3(offsetx / 200.0f, 0, Time.deltaTime) * velocity;
        }

        if (ray && hitInfo.collider.name != "Change") changed = false;

    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.name == "pincho")
        {
            startTimerPincho = true;
        }

        if (collision.collider.name == "fango")
        {
            velocity = 2.5f * 0.66f;
        }
        else velocity = 2.5f;

        jumpCounter = 0;
        myAnimator.SetInteger("jumpCounter", jumpCounter);
        onFloor = true;
        myAnimator.SetBool("onFloor", onFloor);
    }
}
