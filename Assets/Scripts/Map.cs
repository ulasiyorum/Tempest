using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private int mapId; // This mapId will tell us which prefab it's used for
    private List<Collectable> collectables;
    private Graph<Map> connectedMaps;

    private void Start()
    {
        InitCollectables();
    }


    public Graph<Map> ConnectedMaps 
    {
        get 
        {
            connectedMaps ??= new Graph<Map>();

            return connectedMaps;
        }
            
    }
    
    public void Init(int mapId, List<Collectable> collectables)
    {
        this.mapId = mapId;
        this.collectables = collectables;

        //if (collectables == null || collectables.Count == 0)
            

    }


    private void InitCollectables()
    {
        collectables = new List<Collectable>();

        int size = Random.Range(0,5);

        for(int i = 0; i < size; i++)
        {
            collectables.Add(
                new Collectable()
                );
        }

        foreach(var c in collectables)
        {
            GameObject col = Instantiate(AssetsHandler.Instance.collectablePrefab, GameHandler.Instance.Canvas.transform, true);
            col.AddComponent<Collectable>();
            col.GetComponent<Collectable>().Init(this);
            Debug.Log(col.GetComponent<Collectable>() == null);
            col.transform.position = col.GetComponent<Collectable>().position;
            col.GetComponent<SpriteRenderer>().sprite = AssetsHandler.Instance.CollectableAssets[col.GetComponent<Collectable>().ID];
            
        }
    }


    public void Connect(Map connect)
    {
        ConnectedMaps.AddVertex(connect);
        ConnectedMaps.AddEdge(connect, this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MapAlgorithm.ChangeMap();
    }

    public Vector2 GetRandomPointOnTheMap()
    {
        Vector2 current = transform.position;
        Bounds b = GetComponent<BoxCollider2D>().bounds;

        int random = 0;
        do
        {
            random = Random.Range(-1, 1);
        } while (random == 0);

        float height = b.size.y / 2 * random;
        float width = b.size.x / 2 * random;

        height -= Random.Range(0, height);
        width -= Random.Range(0, width);

        return new Vector2(current.x - width, current.y - height);
    }
}
