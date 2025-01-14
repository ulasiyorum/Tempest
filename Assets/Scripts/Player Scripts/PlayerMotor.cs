using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private int direction;
    private Animator anim;

    private void Start()
    {
        direction = 1;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(!Player.consuming)
            HandleMovement();
    }

    public void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float horizontal = transform.position.x + h * Time.deltaTime * 3.2f;
        float vertical = transform.position.y + v * Time.deltaTime * 3.2f;

        switch (h,v)
        {
            case ( > 0, > 0):
            case ( > 0, 0):
            case ( > 0, < 0):
                anim.Play("Character_Move_1");
                direction = 1;
                break;
            case (0, 0):
                if (direction == 1)
                    anim.Play("Character_Idle");
                else
                    anim.Play("Character_Idle_Left");
                break;
            case (0, > 0):
            case (0, < 0):
                if (direction == 1)
                    anim.Play("Character_Move_1");
                else
                    anim.Play("Character_Move_2");
                break;
            case ( < 0, < 0):
            case ( < 0, > 0):
            case ( < 0, 0):
                direction = -1;
                anim.Play("Character_Move_2");
                break;
            default:
                break;
        }


        transform.position = new Vector2(horizontal,vertical);
    }

}
