using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
    public float jumpingForce;
    private bool left, changed;
    private Movement currentMovement;
    private Rigidbody myRigidbody;
    private bool moving, eatingSound;
    private int jumpCounter;
    private float jumpTimer, soundTimer, bloodTimer;
    public ParticleSystem blood;
    void Start()
    {
        left = false;
        changed = false;
        moving = true;
        currentMovement = Movement.FORWARD;
        myRigidbody = GetComponent<Rigidbody>();
        jumpCounter = 0;
        soundTimer = 10;
        eatingSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            bool ray = Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo);

            if (ray)
            {

                if (!changed && hitInfo.collider.name == "Change" && InCenter(hitInfo.collider.transform))
                {
                    left = !left;
                    changed = true;
                    float yEnemy = transform.position.y;
                    transform.position = hitInfo.collider.transform.position;
                    transform.position = new Vector3(transform.position.x, yEnemy, transform.position.z);
                }

                Vector3 nextBlock = transform.position + Vector3.up;
                if (left) nextBlock.x += 0.5f;
                else nextBlock.z += 0.5f;

                if (!Physics.Raycast(nextBlock, Vector3.down) && jumpCounter == 0)
                {
                    myRigidbody.velocity = Vector3.up * jumpingForce;
                    ++jumpCounter;
                    jumpTimer = 0.7f;
                }

            }
            else
            {
                if(jumpCounter < 2 && jumpTimer <= 0)
                {
                    myRigidbody.velocity = Vector3.up * jumpingForce;
                    ++jumpCounter;
                }
            }

            if (left)
            {
                if (currentMovement != Movement.LEFT)
                {
                    currentMovement = Movement.LEFT;
                    transform.Rotate(0, 90, 0);
                }
                transform.position += new Vector3(Time.deltaTime, 0, 0) * velocity;
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

            if (soundTimer <= 0 )
            {
                if (Random.value >= 0.6f)
                {
                    FindObjectOfType<AudioManager>().PlayAtPoint("zombie", transform.position);
                    soundTimer = 10;
                }
                else soundTimer = 2;
            }

        }
        else
        {
            if (!eatingSound)
            {
                FindObjectOfType<AudioManager>().Play("eating");
                eatingSound = true;
            }

            if (bloodTimer <= 0)
            {
                CreateBlood();
                bloodTimer = 1.5f;
            }
        }

        jumpTimer -= Time.deltaTime;

        soundTimer -= Time.deltaTime;

        bloodTimer -= Time.deltaTime;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Player")
        {
            moving = false;
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            if(player.isPlaying) player.EndGame(Deaths.ZOMBIE);
            GetComponentInChildren<Animator>().SetBool("biting", true);
        }

        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo);

        if (hitInfo.collider == collision.collider)
        {
            jumpCounter = 0;
        }

    }

    void CreateBlood()
    {
        blood.Play();
    }

}
