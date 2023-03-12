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

    private void Awake()
    {
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
