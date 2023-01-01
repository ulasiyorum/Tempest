using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Vertex<T>
{
    private T value;
    private List<Vertex<T>> neighbors;

    public Vertex(T value)
    {
        this.value = value;
        this.neighbors = new List<Vertex<T>>();
    }

    public IList<Vertex<T>> Neighbors
    {
        get => neighbors.AsReadOnly();
    }

    public T Value
    {
        get => value;
    }

    public bool AddNeighbors(Vertex<T> neighbor)
    {
        if (neighbors.Contains(neighbor))
            return false;

        neighbors.Add(neighbor);
        return true;
    }

    public bool RemoveNeighbors(Vertex<T> neighbor)
    {
        return neighbors.Remove(neighbor);
    }

    public bool RemoveAllNeighbors()
    {
        for(int i = neighbors.Count; i >= 0; i--)
        {
            neighbors.RemoveAt(i);
        }
        return true;
    }

    public override string ToString()
    {
        StringBuilder vertexString = new StringBuilder();
        vertexString.Append("{ Node Value " + value + " with Neighbors : ");

        foreach(var n in neighbors)
        {
            vertexString.Append(n.value + " ");
        }
        vertexString.Append("}");

        return vertexString.ToString();
    }


}
