using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    public static GameHandler Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameHandler>();

            return instance;
        }
    }


    [SerializeField] Canvas canvas;
    [SerializeField] Player player;

    public Canvas Canvas { get => canvas; }
    public Player Player { get => player; }
}
