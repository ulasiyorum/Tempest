using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MapAlgorithm : MonoBehaviour
{
    private void Update()
    {
        if (!IsInCurrentMap())
        {
            ChangeMap();
        }
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

        GameObject generatedMap = Instantiate(MapPrefabs[random], GameHandler.Instance.Canvas.transform, false);
        generatedMap.transform.position = generatedMapPosition;

        generatedMap.GetComponent<Map>().Connect(current);

        current = generatedMap.GetComponent<Map>();
    }

    public async static void ChangeMap()
    {
        Map x = MapExists(GameHandler.Instance.Player.transform.position);
        Debug.Log(x == null);
        if (x != null)
            current = x;
        else
            CreateMap();
        await Task.Delay(500);
    }

    public static Map MapExists(Vector2 position)
    {
        List<Vertex<Map>> ignoreList = new List<Vertex<Map>>();

        foreach (Vertex<Map> vertex in current.ConnectedMaps.Vertices)
        {
            if (IsInMap(vertex.Value, position))
                return vertex.Value;

            ignoreList.Add(vertex);

            if (ignoreList.Contains(vertex))
                return MapExists(vertex, position, ignoreList);
        }


        return null;
    }

    private static Map MapExists(Vertex<Map> vertex, Vector2 mapLocation, List<Vertex<Map>> ignoreList)
    {
        foreach (var ver in vertex.Neighbors.Values)
        {
            if (IsInMap(vertex.Value, mapLocation))
                return ver.Value;

            ignoreList.Add(vertex);

            if (!ignoreList.Contains(vertex))
                return MapExists(vertex, mapLocation, ignoreList);
        }

        return null;
    }
}