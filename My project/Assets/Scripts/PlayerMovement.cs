using UnityEngine;
using UnityEngine.SceneManagement;


public enum Movement
{
    FORWARD, LEFT
}

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem polvo;
    public float velocity;
    public float jumpingForce;
    private Rigidbody myRigidbody;
    private Animator myAnimator;
    [HideInInspector]
    public bool isPlaying;
    private bool left, changed;
    private int jumpCounter;
    [HideInInspector]
    public bool godMode;
    private bool onFloor, startTimerPincho, audioPlayed;
    private float timerPincho, fallTimer, jumpTimer;
    private Score score;
    private GameManager gameManager;
    public Movement currentMovement { get; private set; }

    private float offsetx, offsetz;

    public GameObject diamante;

    bool first;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.GetChild(0).GetComponent<Animator>();
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        score = FindObjectOfType<Score>();
        left = changed = startTimerPincho = godMode = false;
        onFloor = isPlaying = true;
        timerPincho = Constants.PINCHO_TIMER;
        jumpCounter = 0;
        offsetx = offsetz = 0;
        currentMovement = Movement.FORWARD;
        gameManager = FindObjectOfType<GameManager>();
        fallTimer = Constants.FALL_TIMER;
        jumpTimer = 0.2f;
        first = true;
        audioPlayed = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (godMode)
        {
            GodModeMovement();
        }
        else
        {
            NormalMovement();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
            if (godMode) FindObjectOfType<Score>().SetComment("Set gamemode to Creative Mode");
            else FindObjectOfType<Score>().SetComment("Set gamemode to Survival Mode");

        }

        //Chapuza porque al principio me salia el personaje desplazado porque cogia un offset raro
        first = false;

        if(!audioPlayed && onFloor)
        {
            FindObjectOfType<AudioManager>().Play("running");
            audioPlayed = true;
        }

        if (!onFloor || !isPlaying)
        {
            FindObjectOfType<AudioManager>().Stop("running");
            audioPlayed = false;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {

        if(!godMode && collision.collider.name == "pincho")
        {
            startTimerPincho = true;
        }

        if (!godMode && collision.collider.name == "fango")
        {
            velocity = Constants.Speed * Constants.Slowdown;
            myAnimator.speed = myAnimator.speed * Constants.Slowdown;
        }
        else
        {
            myAnimator.speed = 1;
            velocity = Constants.Speed;
        }

        Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo);

        //if(hitInfo.collider == collision.collider) 
        //{
            jumpCounter = 0;
            myAnimator.SetInteger("jumpCounter", jumpCounter);
            onFloor = true;
            myAnimator.SetBool("onFloor", onFloor);
        //}

    }

    public void EndGame(Deaths death)
    {
        isPlaying = false;
        myAnimator.SetBool("dead", true);
        FindObjectOfType<AudioManager>().Play("damage");

        transform.Rotate(90, 0, 0);

        FindObjectOfType<EndScript>().EndGame(death);

        System.Random r = new System.Random();
        int value = r.Next(1, 5);
        for(int i = 0; i < value; i++)
        {
            GameObject obj = Instantiate(diamante);
            obj.transform.position = transform.position + Vector3.up;
            obj.GetComponent<BoxCollider>().isTrigger = false;
            obj.GetComponent<DiamanteScript>().enabled = false;
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            int valuex = r.Next(-2, 2);
            int valuez = r.Next(-2, 2);

            obj.GetComponent<Rigidbody>().velocity = new Vector3(valuex,4,valuez);
        }

    }

    private bool InCenter(Transform collider)
    {
        float x, z;

        Vector3 center = collider.position;

        x = transform.position.x;
        z = transform.position.z;

        if (Mathf.Abs(center.x - x) < 0.1f && Mathf.Abs(center.z - z) < 0.1f) return true;
        return false;

    }

    private void NormalMovement()
    {
        if (isPlaying)
        {

            if (fallTimer <= 0)
            {
                gameManager.EndGame(Deaths.FALL);
                return;
            }

            if (startTimerPincho) timerPincho -= Time.deltaTime;

            if (startTimerPincho && timerPincho <= 0)
            {
                startTimerPincho = false;
                jumpCounter = 2;

                gameManager.EndGame(Deaths.PINCHO);

            }

            bool ray = Physics.Raycast(transform.position + new Vector3(0, 1f, 0), Vector3.down, out RaycastHit hitInfo);

            if (!ray)
            {
                onFloor = false;
                fallTimer -= Time.deltaTime;
            }

            if (ray && !first)
            {
                offsetx = hitInfo.collider.transform.position.x - transform.position.x;
                offsetz = hitInfo.collider.transform.position.z - transform.position.z;
            }
            else
            {
                offsetx = 0; offsetz = 0;
            }


            if (ray)
            {

                fallTimer = Constants.FALL_TIMER;

                string name = hitInfo.collider.name;

                if (!changed && onFloor && name == "Change")
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        left = !left;
                        changed = true;
                        FindObjectOfType<CreateLevel>().ChangeBlock(hitInfo.collider.transform);
                        score.UpdateScore(score.GetScore() + 1);
                        FindObjectOfType<AudioManager>().Play("change");
                        CreateDust();
                    }
                }
                else
                {
                    if (jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
                    {

                        if (jumpCounter < 1)
                        {
                            myRigidbody.velocity = Vector3.up * jumpingForce;
                            CreateDust();
                        }
                        else myRigidbody.velocity = Vector3.up * jumpingForce * 0.75f;

                        jumpCounter++;
                        myAnimator.SetInteger("jumpCounter", jumpCounter);
                        onFloor = false;
                        myAnimator.SetBool("onFloor", onFloor);
                        FindObjectOfType<AudioManager>().Play("jump");

                    }
                }

            }
            else
            {

                if (jumpCounter > 0 && jumpCounter < 2 && Input.GetKeyDown(KeyCode.Space))
                {

                    if (jumpCounter < 1) myRigidbody.velocity = Vector3.up * jumpingForce;
                    else myRigidbody.velocity = Vector3.up * jumpingForce * 0.75f;

                    jumpCounter++;
                    myAnimator.SetInteger("jumpCounter", jumpCounter);
                    onFloor = false;
                    myAnimator.SetBool("onFloor", onFloor);
                    FindObjectOfType<AudioManager>().Play("jump");
                }

            }

            if (left)
            {
                if (currentMovement != Movement.LEFT)
                {
                    currentMovement = Movement.LEFT;
                    transform.Rotate(0, 90, 0);
                }

                transform.position += new Vector3(Time.deltaTime, 0, offsetz / 200.0f) * velocity;
            }
            else
            {
                if (currentMovement != Movement.FORWARD)
                {
                    currentMovement = Movement.FORWARD;
                    transform.Rotate(0, -90, 0);
                }

                transform.position += new Vector3(offsetx / 200.0f, 0, Time.deltaTime) * velocity;
            }

            if (changed && ray && hitInfo.collider.name != "Change")
            {
                changed = false;
            }

        }

    }

    private void GodModeMovement()
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

                FindObjectOfType<CreateLevel>().ChangeBlock(hitInfo.collider.transform);
                score.UpdateScore(score.GetScore() + 1);
                FindObjectOfType<AudioManager>().Play("change");
                CreateDust();

            }

            Vector3 nextBlock = transform.position + Vector3.up;
            if (left) nextBlock.x += 0.5f;
            else nextBlock.z += 0.5f;

            bool rayNext = Physics.Raycast(nextBlock, Vector3.down);

            if (!rayNext && jumpCounter == 0)
            {
                myRigidbody.velocity = Vector3.up * jumpingForce;
                ++jumpCounter;
                jumpTimer = 0.8f;

                myAnimator.SetInteger("jumpCounter", jumpCounter);
                onFloor = false;
                myAnimator.SetBool("onFloor", onFloor);
                FindObjectOfType<AudioManager>().Play("jump");
                CreateDust();

            }

        }
        else
        {
            if(jumpCounter < 2 && jumpTimer <= 0)
            {
                myRigidbody.velocity = Vector3.up * jumpingForce;
                ++jumpCounter;

                myAnimator.SetInteger("jumpCounter", jumpCounter);
                onFloor = false;
                myAnimator.SetBool("onFloor", onFloor);
                FindObjectOfType<AudioManager>().Play("jump");
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

        jumpTimer -= Time.deltaTime;
    }

    void CreateDust()
    {
        polvo.Play();
    }
}
