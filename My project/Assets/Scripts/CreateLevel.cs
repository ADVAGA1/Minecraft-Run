using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreateLevel : MonoBehaviour
{
    public List<GameObject> threePlatforms;
    public List<GameObject> fourPlatforms;
    public List<GameObject> fivePlatforms;
    public List<GameObject> sixPlatforms;
    public GameObject stairs;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj;
        bool left = false;
        float height = 0;
        float width = 0;
        float depth = 0;
        int nPlatforms;

        obj = Instantiate(sixPlatforms[0]);
        obj.transform.localScale *= scale;
        obj.transform.Translate(width * scale, -depth * scale, height * scale);
        obj.transform.parent = transform;
        height += 3.2f * 6;
        left = !left;

        for (uint i = 0; i < 30; ++i)
        {

            int nThreePlatforms = Random.Range(0, threePlatforms.Count);
            int nFourPlatforms = Random.Range(0, fourPlatforms.Count);
            int nFivePlatforms = Random.Range(0, fivePlatforms.Count);
            int nSixPlatforms = Random.Range(0, sixPlatforms.Count);
            List<GameObject> platforms = new List<GameObject>() { threePlatforms[nThreePlatforms], fourPlatforms[nFourPlatforms], fivePlatforms[nFivePlatforms], sixPlatforms[nSixPlatforms] };

            nPlatforms = Random.Range(3, 7);

            obj = Instantiate(platforms[nPlatforms - 3]);

            obj.transform.localScale *= scale;
            obj.transform.Translate(width * scale, -depth * scale, height * scale);
            if (left)
            {
                obj.transform.Translate(3.2f * scale, 0, -3.2f * scale);
                obj.transform.Rotate(0.0f, 90.0f, 0.0f);
            }

            obj.transform.parent = transform;

            if (left) width += 3.2f * nPlatforms;
            else height += 3.2f * nPlatforms;

            if (i < 14 && Random.value >= 0.5) 
            {
                GameObject stair = Instantiate(stairs);
                
                stair.transform.localScale *= scale;
                stair.transform.Translate(width * scale, -depth * scale + 1.6f*scale, height * scale);
                stair.transform.Rotate(0.0f, 90.0f, 0.0f);

                if (!left)
                {
                    stair.transform.Translate(3.2f*scale, 0, 3.2f*scale); 
                    stair.transform.Rotate(0.0f, 90.0f, 0.0f);
                }

                stair.transform.parent = transform;

                depth += 1.6f;

                if (left) height += 3.2f;
                else width += 3.2f;

            }


            left = !left;
        }
    }
}
