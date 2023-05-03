using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocity;
    public float jumpingForce;
    public Rigidbody myRigidbody;
    private bool left, changed;
    private int jumpCounter;

    private float offsetx, offsetz;

    // Start is called before the first frame update
    void Start()
    {
        left = false;
        changed = false;
        jumpCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        bool ray = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);

        if (ray && !changed && hitInfo.collider.name == "Change")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                left = !left;
                changed = true;
                
                offsetx = hitInfo.collider.transform.position.x - transform.position.x;
                offsetz = hitInfo.collider.transform.position.z - transform.position.z;

                transform.position += new Vector3(offsetx, 0, offsetz);

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

        if (left) transform.position += new Vector3(Time.deltaTime, 0, 0) * velocity;
        else transform.position +=  new Vector3(0, 0, Time.deltaTime) * velocity;

        if (ray && hitInfo.collider.name != "Change") changed = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCounter = 0;
    }
}
