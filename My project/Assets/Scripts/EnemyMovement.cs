using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
    public float jumpingForce;
    private bool left, changed;
    private Movement currentMovement;
    private Rigidbody myRigidbody;
    void Start()
    {
        left = false;
        changed = false;
        currentMovement = Movement.FORWARD;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ray = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo);

        if (ray)
        {

            Vector3 nextBlock = transform.position;

            if (left) nextBlock.x += 0.4f;
            else nextBlock.z += 0.4f;

            if(!changed && hitInfo.collider.name == "Change" && InCenter(hitInfo.collider.transform))
            {
                left = !left;
                changed = true;
                float yEnemy = transform.position.y;
                transform.position = hitInfo.collider.transform.position;
                transform.position = new Vector3(transform.position.x, yEnemy, transform.position.z);
            }

            if(hitInfo.collider.name != "Change" && !Physics.Raycast(nextBlock, Vector3.down))
            {
                myRigidbody.velocity = Vector3.up * jumpingForce;
            } 
            
        }

        if (left)
        {
            if (currentMovement != Movement.LEFT)
            {
                currentMovement = Movement.LEFT;
                transform.Rotate(0, 90, 0);
            }
            transform.position += new Vector3(Time.deltaTime, 0,0) * velocity;
        }
        else
        {
            if (currentMovement != Movement.FORWARD)
            {
                currentMovement = Movement.FORWARD;
                transform.Rotate(0, -90, 0);
            }
            transform.position += new Vector3(0, 0, Time.deltaTime) * velocity;
        }

        if (ray && hitInfo.collider.name != "Change") changed = false;

    }

    private bool InCenter(Transform collider)
    {
        float x, z;

        Vector3 center = collider.position;

        x = transform.position.x;
        z = transform.position.z;

        if(Mathf.Abs(center.x - x) < 0.1f && Mathf.Abs(center.z - z) < 0.1f) return true;
        return false;

    }

}