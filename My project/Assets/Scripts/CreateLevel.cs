using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public GameObject groundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj;
        bool left = false;
        uint height = 0;
        uint width = 0;
        uint scale = 1;

        for(uint i = 0; i<2; ++i){

            obj = (GameObject)Instantiate(groundPrefab);

            obj.transform.localScale *= scale;
            if(left) obj.transform.Rotate(0.0f,90.0f,0.0f);
            obj.transform.Translate(0.0f,0.0f,0.0f);

            obj.transform.parent = transform;

            if(left) width += 5;
            else height += 5;

            left = !left;

        }
    }
}
