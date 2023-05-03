using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public GameObject groundPrefab;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj;
        bool left = false;
        uint height = 0;
        uint width = 0;

        for(uint i = 0; i<15; ++i){

            obj = (GameObject)Instantiate(groundPrefab);

            obj.transform.localScale *= scale;
            obj.transform.Translate(width*scale, 0.0f, height*scale);
            if (left)
            {
                obj.transform.Translate(2*scale, 0, -2 * scale);
                obj.transform.Rotate(0.0f, 90.0f, 0.0f);
            }

            obj.transform.parent = transform;

            if(left) width += 6;
            else height += 6;

            left = !left;
        }
    }
}
