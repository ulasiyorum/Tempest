using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public Player player;

    private void Start()
    {
        i = this;
    }
}
