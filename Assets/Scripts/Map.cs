using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    private int mapId; // This mapId will tell us which prefab it's used for
    private List<Collectable> collectables;
    private Graph<Map> connectedMaps;

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
        float height = b.size.y;
        float width = b.size.x;

        height -= Random.Range(0, height);
        width -= Random.Range(0, width);

        return new Vector2(current.x - width, current.y - height);
    }
}
