using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Graph<T>
{
    private List<Vertex<T>> vertices = new List<Vertex<T>>();

    public int Count
    {
        get => vertices.Count;
    }

    public IList<Vertex<T>> Vertices
    {
        get => vertices.AsReadOnly();
    }

    public bool AddVertex(T val)
    {
        if (Find(val) != null)
            return false;

        vertices.Add(new Vertex<T>(val));
        return true;
    }

    public Vertex<T> Find(T val)
    {
        foreach(Vertex<T> vertex in vertices)
        {
            if (vertex.Value.Equals(val))
            {
                return vertex;
            }
        }

        return null;
    }

    public bool AddEdge(T val1, T val2)
    {
        Vertex<T> ver1 = Find(val1);
        Vertex<T> ver2 = Find(val2);

        if (val1 == null || val2 == null)
            return false;

        if (ver1.Neighbors.Contains(ver2))
            return false;

        ver1.AddNeighbors(ver2);
        ver2.AddNeighbors(ver1);
        return true;
    }

    public bool RemoveVertex(T val)
    {
        Vertex<T> remove = Find(val);
        if (remove == null)
            return false;

        vertices.Remove(remove);
        foreach (Vertex<T> ver in vertices)
            ver.RemoveNeighbors(remove);

        return true;
    }

    public bool RemoveEdge(T val1, T val2)
    {
        Vertex<T> ver1 = Find(val1);
        Vertex<T> ver2 = Find(val2);
        if (ver1 == null || ver2 == null)
            return false;

        if (!ver1.Neighbors.Contains(ver2))
            return false;

        ver1.RemoveNeighbors(ver2);
        ver2.RemoveNeighbors(ver1);
        return true;
    }

    public void Clear()
    {
        foreach(Vertex<T> ver in vertices)
            ver.RemoveAllNeighbors();

        for (int i = vertices.Count - 1; i >= 0; i--)
            vertices.RemoveAt(i);
    }

    public override string ToString()
    {
        StringBuilder verString = new StringBuilder();
        for(int i = 0; i < Count; i++)
        {
            verString.Append(vertices[i].ToString());
            if (i < Count - 1)
            {
                verString.Append("\n");
            }
        }
        return verString.ToString();
    }
}
