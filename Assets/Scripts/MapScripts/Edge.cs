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
    public Transform map;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "PlayerObject")
            return;

        if (alreadyCreated)
            return;

        alreadyCreated = true;
        GameManager.i.CreateMap(type,map.position);

    }
}
