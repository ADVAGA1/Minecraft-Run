using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runspeed = 5f;
    [SerializeField] private float jumpHeight = 2f;

    private float gravity = -50f;
    private CharacterController characterController;
    private Vector3 velocity;
    private bool ground;
    private float horizontalInput = 1f;
    private bool direction = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        ground = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore); 

        if (Input.GetKeyDown(KeyCode.Space) && direction) {
            direction = false; // Change direction
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !direction){
            direction = true;

        }

        //salto
        if (ground && Input.GetKeyDown(KeyCode.X)) {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //gravedad
        if (ground && velocity.y < 0) {
            velocity.y = 0;
        }
        else {
            velocity.y += gravity * Time.deltaTime;
        }

        //cambio de direccion
        if (direction) {
            characterController.Move(new Vector3(0, 0, horizontalInput * runspeed) * Time.deltaTime);
        }
        else {
            characterController.Move(new Vector3(horizontalInput * runspeed, 0, 0) * Time.deltaTime);
            
        }

        //salto



        characterController.Move(velocity * Time.deltaTime);
      */
    }
}
