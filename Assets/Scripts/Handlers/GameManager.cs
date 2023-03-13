using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public Player player;
    public Canvas canvas;
    public Map current;

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
        CreateMap(Edge.Type.Start);
        Vector2 bounds = current.GetComponent<BoxCollider2D>().bounds.size;
        Map.ySize = bounds.y;
        Map.xSize = bounds.x;
    }

    public void CreateMap(Edge.Type type)
    {
        GameObject[] prefabs = AssetsHandler.i.mapPrefabs;
        int random = UnityEngine.Random.Range(0,prefabs.Length);
        CreateNewMap(prefabs[random], type);
    }

    private void CreateNewMap(GameObject prefab, Edge.Type type)
    {
        Vector2 currentPos = Vector2.zero;

        if (type != Edge.Type.Start)
            currentPos = current.transform.position;

        Vector2 newPos = type switch
        {
            Edge.Type.Start => Vector3.zero,
            Edge.Type.Left => new Vector3(currentPos.x - Map.xSize, currentPos.y),
            Edge.Type.Right => new Vector3(currentPos.x + Map.xSize, currentPos.y),
            Edge.Type.Bottom => new Vector3(currentPos.x, currentPos.y - Map.ySize),
            Edge.Type.Top => new Vector3(currentPos.x, currentPos.y + Map.ySize),
            _ => Vector3.zero
        };
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
