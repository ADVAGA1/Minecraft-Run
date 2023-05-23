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
    public GameObject diamonds;
    public GameObject suelo_change;
    public GameObject trees;
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
        height += Constants.blockSize * 6;
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
                obj.transform.Translate(Constants.blockSize * scale, 0, -Constants.blockSize * scale);
                obj.transform.Rotate(0.0f, 90.0f, 0.0f);
            }

            obj.transform.parent = transform;

            if (left) width += Constants.blockSize * nPlatforms;
            else height += Constants.blockSize * nPlatforms;

            //Diamond spawn
            if (Random.value >= 0.8)
            {
                GameObject diamond = Instantiate(diamonds);

                DiamanteScript diamanteScript = diamond.GetComponent<DiamanteScript>();

                diamond.transform.parent = obj.transform;
                diamond.transform.position = obj.transform.GetChild(0).position;
                diamond.transform.Translate(0, diamanteScript.animationAmplitude + Constants.blockSize * scale, 0);

            }


            //Stair spawn
            if (i < 14 && Random.value >= 0.6) 
            {
                GameObject stair = Instantiate(stairs);
                
                stair.transform.localScale *= scale;
                stair.transform.Translate(width * scale, -depth * scale + Constants.blockSize / 2.0f *scale, height * scale);
                stair.transform.Rotate(0.0f, 90.0f, 0.0f);

                if (!left)
                {
                    stair.transform.Translate(Constants.blockSize*scale, 0, Constants.blockSize * scale); 
                    stair.transform.Rotate(0.0f, 90.0f, 0.0f);
                }

                stair.transform.parent = transform;

                depth += Constants.blockSize / 2.0f;

                if (left) height += Constants.blockSize;
                else width += Constants.blockSize;

            }

            //Tree spawn
            if (Random.value >= 0.5)
            {
                GameObject tree = Instantiate(trees);

                tree.transform.localScale *= scale;

                tree.transform.position = obj.transform.Find("Change").position;

                if (left)
                {
                    tree.transform.Translate(-Constants.blockSize * scale * (nPlatforms/2 - 1), -Constants.blockSize * 1.5f * scale, -Constants.blockSize * (nPlatforms/2 + 2)* scale);
                }
                else tree.transform.Translate(-Constants.blockSize * scale * (nPlatforms/2 + 2), -Constants.blockSize * 1.5f * scale, -Constants.blockSize * (nPlatforms / 2 - 1) * scale);

                tree.transform.parent = obj.transform;

            }

            left = !left;
        }
    }

    public void ChangeBlock(Transform block)
    {
        GameObject suelo = Instantiate(suelo_change);

        suelo.transform.localScale *= scale;
        suelo.transform.position = block.position;
        suelo.transform.parent = block.parent;
        block.gameObject.SetActive(false);
        suelo.name = block.name;
    }

}
