using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float velocity;
    public GameObject player;
    private Vector3 offsetCamera;
    private Transform origin, end;
    // Start is called before the first frame update
    void Start()
    {

        offsetCamera = transform.position;
        origin = new GameObject().transform;
        end = new GameObject().transform;

    }

    // Update is called once per frame
    void Update()
    {

        bool ray = Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit hitInfo);

        if (ray)
        {
            if (hitInfo.collider.name == "escalera(Clone)")
            {
                int i = hitInfo.collider.transform.GetSiblingIndex();
                origin = hitInfo.collider.transform.parent.GetChild(i + 1).GetChild(0);
            }
            else if(hitInfo.collider.name == "diamante(Clone)")
            {
                origin = hitInfo.collider.transform.parent.GetChild(0);
            }
            else
            {
                origin = hitInfo.collider.transform.parent.GetChild(0);
            }

            int j = origin.parent.GetSiblingIndex();

            if (j + 1 < origin.parent.parent.childCount)
            {

                Transform next = origin.parent.parent.GetChild(j + 1);  //prefab siguiente

                if (next.name != "escalera(Clone)")
                {
                    end = next.GetChild(0);
                }
                else
                {
                    int k = next.GetSiblingIndex();
                    end = next.parent.GetChild(k + 1).GetChild(0);
                }
            }
        }

        Vector3 mid = (origin.position + end.position) / 2;

        Vector3 move = (mid - transform.position) + offsetCamera;

        move = move.normalized;

        var currentRotation = transform.rotation;
        transform.rotation = Quaternion.identity;

        // if(!Shaking(move, mid)) 
        transform.Translate(move * Time.deltaTime * velocity);

        transform.rotation = currentRotation;
    }

    private bool Shaking(Vector3 move, Vector3 end) 
    {
        Vector3 nextPosition = transform.position + move * Time.deltaTime * velocity;
        if (nextPosition.x > end.x && nextPosition.y > end.y && nextPosition.z > end.z) return false;
        return true;
    }

}
