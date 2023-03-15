using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public Player player;
    public Canvas canvas;
    public Canvas UICanvas;
    public Map current;

    public List<Vector2> positions;

    public void ChangeCurrentMap(Map newMap)
    {
        current = newMap;
    }

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        CreateMap(Edge.Type.Start, Vector2.zero);
        Vector2 bounds = current.GetComponent<BoxCollider2D>().bounds.size;
        Map.ySize = bounds.y;
        Map.xSize = bounds.x;
    }

    public void CreateMap(Edge.Type type, Vector2 position)
    {
        GameObject[] prefabs = AssetsHandler.i.mapPrefabs;
        int random = UnityEngine.Random.Range(0,prefabs.Length);
        CreateNewMap(prefabs[random], type, position);
    }

    private bool MapExists(Vector2 position)
    {
        foreach(Vector2 pos in positions)
        {
            if(Vector2.Distance(pos,position) < 15f)
            {
                Debug.Log(Vector2.Distance(pos, position));
                return true;
            }
        }
        return false;
    }

    private void CreateNewMap(GameObject prefab, Edge.Type type,Vector2 position)
    {
        Vector2 newPos = type switch
        {
            Edge.Type.Start => Vector3.zero,
            Edge.Type.Left => new Vector3(position.x - Map.xSize, position.y),
            Edge.Type.Right => new Vector3(position.x + Map.xSize, position.y),
            Edge.Type.Bottom => new Vector3(position.x, position.y - Map.ySize),
            Edge.Type.Top => new Vector3(position.x, position.y + Map.ySize),
            _ => Vector3.zero
        };
        
        if (MapExists(newPos))
            return;

        positions.Add(newPos);

        
        GameObject go = Instantiate(prefab, canvas.transform, true);
        go.GetComponent<Map>().CancelEdge(type);

        if (type == Edge.Type.Start)
            ChangeCurrentMap(go.GetComponent<Map>());

        go.transform.position = newPos;
    }

    private void Update()
    {
    }
}
