using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] Transform player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
