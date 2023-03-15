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

    private List<Collectables> inventory;

    private void Start()
    {
        inventory = new List<Collectables>();
    }
    public List<Collectables> Inventory { get => inventory; }

    public void Collect(Collectables item)
    {
        carrying += item.itemWeight;
        inventory.Add(item);
    }

    public void Drop(Collectables collectable)
    {
        inventory.Remove(collectable);
    }
}
