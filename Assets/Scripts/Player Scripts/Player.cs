using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float health;
    private float cold;
    private float eat;
    private float drink;
    private float sleep;
    private float carrying;
    public float Carrying { get => carrying; }

    public const int MaxCarry = 40;

    private List<Collectables> inventory = new List<Collectables>();

    public void Collect(Collectables item)
    {
        carrying += item.itemWeight;
        inventory.Add(item);
    }

}
