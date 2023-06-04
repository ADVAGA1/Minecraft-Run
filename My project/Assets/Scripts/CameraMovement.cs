using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float velocity;
    public GameObject player;
    public GameObject level;
    private Vector3 offsetCamera;
    private Transform origin, end;
    private float timer, initialVelocity;
    private bool changed;
    // Start is called before the first frame update
    void Start()
    {

        offsetCamera = transform.position;
        origin = level.transform.GetChild(0).Find("Change");
        end = level.transform.GetChild(1).Find("Change");
        timer = 60;
        changed = false;
        initialVelocity = velocity;

    }

    // Update is called once per frame
    void Update()
    {

        if(timer <= 0)
        {
            changed = !changed;
            if (changed)
            {
                timer = 5;
                velocity = initialVelocity + 0.2f;
            }
            else
            {
                timer = 60;
                velocity = initialVelocity;
            }
        }

        if (FindAnyObjectByType<PlayerMovement>().isPlaying)
        {
            bool ray = Physics.Raycast(player.transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo);

            if (ray)
            {

                if (hitInfo.collider.name == "escalera(Clone)")
                {
                    int i = hitInfo.collider.transform.GetSiblingIndex();
                    origin = hitInfo.collider.transform.parent.GetChild(i + 1).Find("Change");
                }
                else if(hitInfo.collider.name == "Flecha(Clone)")
                {
                    origin = hitInfo.collider.transform.parent.parent.Find("Change");
                }
                else if(hitInfo.collider.name == "bloqueescalera")
                {
                    int i = hitInfo.collider.transform.parent.GetSiblingIndex();
                    origin = hitInfo.collider.transform.parent.parent.GetChild(i + 1).Find("Change");
                }
                else
                {
                    origin = hitInfo.collider.transform.parent.transform.Find("Change");
                }

                int j = origin.parent.GetSiblingIndex();

                if (j + 1 < origin.parent.parent.childCount)
                {

                    Transform next = origin.parent.parent.GetChild(j + 1);  //prefab siguiente

                    if (next.name != "escalera(Clone)")
                    {
                        end = next.Find("Change");
                    }
                    else
                    {
                        int k = next.GetSiblingIndex();
                        end = next.parent.GetChild(k + 1).Find("Change");
                    }
                }
            }
        }

        Vector3 mid = (origin.position + end.position) / 2;

        Vector3 move = (mid - transform.position) + offsetCamera;;

        var currentRotation = transform.rotation;
        transform.rotation = Quaternion.identity;

        velocity = player.GetComponent<PlayerMovement>().velocity * 0.725f;

        if(!Shaking(move, mid)) transform.Translate(move.normalized * Time.deltaTime * velocity);

        transform.rotation = currentRotation;

        timer -= Time.deltaTime;
    }

    private bool Shaking(Vector3 move, Vector3 end) 
    {
        Vector3 nextPosition = transform.position + move.normalized * Time.deltaTime * velocity;

        if (nextPosition.x >= end.x + offsetCamera.x && nextPosition.z >= end.z + offsetCamera.z) return true;
        return false;
    }

}
