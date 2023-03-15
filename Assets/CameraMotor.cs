using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector2 offset;

    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(offset.x + player.transform.position.x,offset.y + player.transform.position.y,transform.position.z);
    }
}
