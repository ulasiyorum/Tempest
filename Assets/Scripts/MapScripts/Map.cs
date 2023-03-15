using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int id;
    public int number;
    
    
    public static int numberOfMaps;
    public static float xSize;
    public static float ySize;

    public Edge[] edges; // 0 up 1 down 2 left 3 right

    private void Start()
    {
        SpawnCollectables();
    }

    private void SpawnCollectables()
    {
        int amount = Random.Range(0,6);
        GameObject[] prefabs = AssetsHandler.i.colPrefabs;
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, prefabs.Length);
            GameObject go = Instantiate(prefabs[index],GameManager.i.canvas.transform,true);
            float xRandom = Random.Range(transform.position.x - xSize / 2, transform.position.x + xSize / 2);
            float yRandom = Random.Range(transform.position.y - ySize / 2, transform.position.y + ySize / 2);
            Vector2 location = new Vector2(xRandom, yRandom);
            go.transform.position = location;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag != "PlayerObject")
            return;

        GameManager.i.ChangeCurrentMap(this);
    }


    public void CancelEdge(Edge.Type type)
    {
        if(type == Edge.Type.Bottom)
        {
            edges[0].alreadyCreated = true;
        }
        else if(type == Edge.Type.Top)
        {
            edges[1].alreadyCreated = true;
        }
        else if(type == Edge.Type.Right)
        {
            edges[2].alreadyCreated = true;
        }
        else if(type == Edge.Type.Left)
        {
            edges[3].alreadyCreated = true;
        }
    }
}
