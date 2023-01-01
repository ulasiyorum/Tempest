using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Vertex<T>
{
    private T value;
    private Dictionary<int, Vertex<T>> neighbors;

    public Vertex(T value)
    {
        this.value = value;
        this.neighbors = new Dictionary<int, Vertex<T>>();
    }

    public IDictionary<int, Vertex<T>> Neighbors
    {
        get => neighbors;
    }

    public T Value
    {
        get => value;
    }

    public bool AddNeighbors(Vertex<T> neighbor)
    {
        if (Neighbors.Values.Contains(neighbor))
            return false;

        neighbors.Add(neighbors.Keys.Count + 1, neighbor);
        return true;
    }

    public bool RemoveNeighbors(Vertex<T> neighbor)
    {
        int key = default;
        foreach(KeyValuePair<int , Vertex<T>> pair in neighbors)
        {
            if (EqualityComparer<Vertex<T>>.Default.Equals(pair.Value, neighbor))
                key = pair.Key;
        }

        return neighbors.Remove(key);
    }

    public bool RemoveAllNeighbors()
    {
        neighbors.Clear();
        return true;
    }

    public override string ToString()
    {
        StringBuilder vertexString = new StringBuilder();
        vertexString.Append("{ Node Value " + value + " with Neighbors : ");

        foreach(var n in neighbors.Values)
        {
            vertexString.Append(n.value + " ");
        }
        vertexString.Append("}");

        return vertexString.ToString();
    }


}
