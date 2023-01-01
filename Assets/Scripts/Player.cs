using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        float horizontal = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * 2.5f;
        float vertical = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * 2.5f;

        transform.position = new Vector2(horizontal,vertical);
    }
}
