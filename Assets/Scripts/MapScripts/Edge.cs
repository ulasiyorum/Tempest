using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public enum Type
    {
        Left,
        Right,
        Bottom,
        Top,
        Start
    }

    public Type type;
    public bool alreadyCreated;
    // Start is called before the first frame update
    void Start()
    {
        alreadyCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerObject")
            return;

        Debug.LogWarning("trigger");
        if (alreadyCreated)
            return;
        alreadyCreated = true;
        GameManager.i.CreateMap(type);

    }
}
