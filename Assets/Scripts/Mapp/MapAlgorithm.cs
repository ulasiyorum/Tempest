using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class MapAlgorithm : MonoBehaviour
{
    private void Update()
    {
        
    }

    private static bool IsInCurrentMap()
    {
        return IsInMap(current,GameHandler.Instance.Player.transform.position);
    }
    private static bool IsInMap(Map map, Vector2 position)
    {
        return map.GetComponent<BoxCollider2D>().bounds.Contains(position);
    }

    private static GameObject[] MapPrefabs 
    {
        get => AssetsHandler.Instance.MapPrefabs;
    }
    private static Map root;
    private static Map current;
    private static Vector2[] mapSpawnPositions
    {
        get
        {
            Vector2 position = current.transform.position;
            Vector2 rectScale = GetRect();
            Vector2[] positions = new Vector2[9];
            float x = -1 * rectScale.x;
            float y = -1 * rectScale.y;

            for(int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(position.x + x , position.y + y);
                x += rectScale.x;
                if((i + 1) % 3 == 0)
                {
                    x = -1 * rectScale.x;
                    y += rectScale.y;
                }
            }
            return positions;
        }
    }

    private static Vector2 GetRect()
    {
        Bounds bounds = current.GetComponent<BoxCollider2D>().bounds;
        float x = Mathf.Abs(bounds.size.x - current.transform.position.x);
        float y = Mathf.Abs(bounds.size.y - current.transform.position.y);
        return new Vector2(x,y);
    }

    private void Start()
    {
        int random = UnityEngine.Random.Range(0, MapPrefabs.Length);
        root = Instantiate(MapPrefabs[random], GameHandler.Instance.Canvas.transform, false).GetComponent<Map>();
        root.transform.position = Vector3.zero;
        current = root;
    }

    public static void CreateMap()
    {
        if (IsInCurrentMap())
            return;

        int random = UnityEngine.Random.Range(0, MapPrefabs.Length);
        float closestDistance = float.MaxValue;
        int closestDistanceIndex = -1;
        Vector2 playerPosition = GameHandler.Instance.Player.transform.position;

        for (int i = 0; i < mapSpawnPositions.Length; i++)
        {
            if (closestDistance > Vector2.Distance(playerPosition, mapSpawnPositions[i]))
            {
                closestDistance = Vector2.Distance(playerPosition, mapSpawnPositions[i]);
                closestDistanceIndex = i;
            }
        }

        Vector2 generatedMapPosition = mapSpawnPositions[closestDistanceIndex];
        Debug.Log(generatedMapPosition);

        GameObject generatedMap = Instantiate(MapPrefabs[random], GameHandler.Instance.Canvas.transform, false);
        generatedMap.transform.position = generatedMapPosition;

        generatedMap.GetComponent<Map>().Connect(current);

        current = generatedMap.GetComponent<Map>();
    }

    public static void ChangeMap()
    {
        Map x = MapExists(GameHandler.Instance.Player.transform.position);
        Debug.Log(x == null);
        if (x != null)
            current = x;
        else
            CreateMap();
    }

    public static Map MapExists(Vector2 position)
    {
        HashSet<Map> visited = new();

        return MapExists(position, current, visited);
    }

    private static Map MapExists(Vector2 position, Map current, HashSet<Map> visited)
    {
        if (visited.Contains(current))
            return null;

        if (IsInMap(current,position))
            return current;

        visited.Add(current);

        foreach(Vertex<Map> vertex in current.ConnectedMaps.Vertices)
        {
            if (MapExists(position, vertex.Value, visited) != null)
                return vertex.Value;
        }

        return null;
    }

}