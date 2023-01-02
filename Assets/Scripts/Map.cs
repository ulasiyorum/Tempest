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
}
