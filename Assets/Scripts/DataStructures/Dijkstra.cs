using System.Collections.Generic;
using System.Collections;


public class Dijkstra<T>
{
    public static void CalculateShortestPath(Graph<T> graph, Vertex<T> source)
    {
        source.distance = 0;
        HashSet<Vertex<T>> settled = new();
        PriorityQueue<Vertex<T>> unsettled = new();
        while(!unsettled.IsEmpty())
        {
            Vertex<T> current = unsettled.Dequeue();
            foreach(KeyValuePair<int, Vertex<T>> pair in current.Neighbors)
            {
                Vertex<T> adj = pair.Value;
                int dist = pair.Key;

                if(!settled.Contains(adj))
                {
                    CalculateMinimumDistance(current,adj,dist);
                    unsettled.Enqueue(adj);
                }
            }
            settled.Add(current);
        }
    }
    
    private static void CalculateMinimumDistance(Vertex<T> source, Vertex<T> destination, int distance)
    {
        if(source.distance + distance < destination.distance)
        {
            destination.distance = source.distance + distance;
            LinkedList<Vertex<T>> shortest = new LinkedList<Vertex<T>>(source.shortestPath);
            shortest.AddLast(source);
            destination.shortestPath = shortest;
        }
    }

}