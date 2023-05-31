using System.Collections.Generic;
using System.Data;
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
    public GameObject terrain;
    public GameObject player;
    public GameObject normalBlock;
    public float scale;

    private float height, width, depth;
    private int timesSpawned, terrainsSpawned, indexTerrain;
    private bool left;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj;
        left = false;
        timesSpawned = 0;
        indexTerrain = 0;
        height = width = depth = 0;

        obj = Instantiate(sixPlatforms[0]);
        obj.transform.localScale *= scale;
        obj.transform.Translate(width * scale, -depth * scale, height * scale);
        obj.transform.parent = transform;
        height += Constants.blockSize * 6;
        left = !left;

        obj = Instantiate(terrain);
        obj.transform.Translate(0, -175 - depth, 0);
        obj.transform.name = "terrain" + indexTerrain.ToString();

        SpawnMap();
    }

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 lastPlatform = transform.GetChild(transform.childCount - 1).position;
        float dist = Vector3.Distance(playerPos, lastPlatform);

        if(dist <= 10)
        {
            SpawnMap();
            ++timesSpawned;
        }

        if(timesSpawned == 2)
        {
            GameObject obj = Instantiate(terrain);
            obj.transform.position = player.transform.position;
            obj.transform.Translate(0, -175 - depth, 0);
            obj.transform.name = "terrain" + indexTerrain.ToString();
            timesSpawned = 0;
            ++terrainsSpawned;
            ++indexTerrain;
        }

        if(terrainsSpawned == 2)
        {
            GameObject t = GameObject.Find("terrain" + (indexTerrain - 2).ToString());
            Destroy(t);
            terrainsSpawned = 0;
        }

    }

    public void ChangeBlock(Transform block)
    {
        GameObject suelo = Instantiate(suelo_change);

        suelo.transform.localScale *= scale;
        suelo.transform.position = block.position;
        suelo.transform.parent = block.parent;
        block.transform.Translate(0, -0.01f, 0);
        //block.gameObject.SetActive(false);
        suelo.name = block.name;
    }

    private void SpawnMap()
    {
        GameObject obj;
        int nPlatforms;

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
                GameObject block = Instantiate(normalBlock);

                stair.transform.localScale *= scale;
                stair.transform.Translate(width * scale, -depth * scale + Constants.blockSize / 2.0f * scale, height * scale);
                stair.transform.Rotate(0.0f, 90.0f, 0.0f);

                block.transform.localScale *= scale;
                block.transform.Translate(width * scale, -depth * scale - Constants.blockSize * scale + Constants.blockSize / 2.0f * scale, height * scale);
                block.transform.Rotate(0.0f, 90.0f, 0.0f);

                if (!left)
                {
                    stair.transform.Translate(Constants.blockSize * scale, 0, Constants.blockSize * scale);
                    stair.transform.Rotate(0.0f, 90.0f, 0.0f);
                    block.transform.Translate(Constants.blockSize * scale, 0, Constants.blockSize * scale);
                    block.transform.Rotate(0.0f, 90.0f, 0.0f);
                }

                stair.transform.parent = transform;
                block.transform.parent = stair.transform;
                block.transform.name = "bloqueescalera";

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
                    tree.transform.Translate(-Constants.blockSize * scale * (nPlatforms / 2 - 1), -Constants.blockSize * 1.5f * scale, -Constants.blockSize * (nPlatforms / 2 + 2) * scale);
                }
                else tree.transform.Translate(-Constants.blockSize * scale * (nPlatforms / 2 + 2), -Constants.blockSize * 1.5f * scale, -Constants.blockSize * (nPlatforms / 2 - 1) * scale);

                tree.transform.parent = obj.transform;

            }

            left = !left;
        }
    }

}
