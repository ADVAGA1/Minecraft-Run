using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserScript : MonoBehaviour
{

    public GameObject arrowPrefab;
    public float speed;
    private bool arrowThrowed;
    private GameObject arrow;
    private Vector3 arrowDirection;

    // Start is called before the first frame update
    void Start()
    {
        arrowThrowed = false;
        arrow = null;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 forward = Quaternion.Euler(0,-90,0) * transform.forward;

        bool ray = Physics.Raycast(transform.position, forward + new Vector3(0, 0.1f, 0), out RaycastHit hitInfo);

        if (ray)
        {
            if(!arrowThrowed && hitInfo.collider.name == "Player")
            {
                arrow = Instantiate(arrowPrefab);
                arrow.transform.position = transform.position + new Vector3(0,0.1f,0);
                arrow.transform.Rotate(0, 90 + transform.parent.rotation.eulerAngles.y, 0);
                arrow.transform.parent = transform;
                arrowThrowed = true;

                if(transform.parent.rotation.eulerAngles.y != 0) arrowDirection = Quaternion.Euler(0,-transform.parent.rotation.eulerAngles.y,0) * arrow.transform.forward;
                else arrowDirection = arrow.transform.forward;

            }

        }

        if (arrow != null)
            arrow.transform.Translate(arrowDirection * Time.deltaTime * speed);

    }
}
