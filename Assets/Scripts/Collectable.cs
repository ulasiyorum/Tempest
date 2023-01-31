using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable
{
    private int id; // To tell us what type of collectable is this
    private float x; // position on current map
    private float y; // position on current map


    public Collectable(Map map)
    {
        id = Random.Range(0, 5);
        x = map.GetRandomPointOnTheMap().x;
        y = map.GetRandomPointOnTheMap().y;
    }
}
