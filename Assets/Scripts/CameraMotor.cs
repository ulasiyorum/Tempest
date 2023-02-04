using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Vector3 offset;
    void Start()
    {
        offset = transform.position;
    }
    void Update()
    {
        Vector2 player = GameHandler.Instance.Player.transform.position;
        transform.position = new Vector3(player.x + offset.x , player.y + offset.y, offset.z);
    }
}
