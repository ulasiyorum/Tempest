using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMotor : MonoBehaviour
{
    public Transform Player;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Player.position + offset;
    }
}
