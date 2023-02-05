using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private int id; // To tell us what type of collectable is this
    private string collectableName;
    private float x; // position on current map
    private float y; // position on current map

    // Attributes

    private float eatAmount;
    private float drinkAmount;
    private float fireAmount;
    private float bonus;

    // Properties

    private bool eatable; // May add or remove drinkAmount
    private bool drinkable; // Comes with bonuses
    private bool flamable;


    public int ID { get => id; }
    public Vector2 position { get => new Vector2(x,y); }



    public void Init(Map map)
    {
        id = Random.Range(0, 5);
        x = map.GetRandomPointOnTheMap().x;
        y = map.GetRandomPointOnTheMap().y;
    }
}