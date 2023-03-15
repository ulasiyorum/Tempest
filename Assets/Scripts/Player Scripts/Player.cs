using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float health;
    private float cold;
    private float eat;
    private float drink;
    private float carrying;
    public float Carrying { get => carrying; }

    public const int MaxCarry = 40;

    private List<Collectables> inventory;


    [SerializeField] Image healthBar;
    [SerializeField] Image warmthBar;
    [SerializeField] Image hungerBar;
    [SerializeField] Image thirstBar;

    private void Start()
    {
        health = 100;
        cold = 100;
        eat = 3000;
        drink = 3000;
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

    public bool UseItem(Collectables item)
    {
        bool used = false;
        switch(item.type)
        {
            case Collectables.Type.Burn:
                break;
            case Collectables.Type.Cloth:
                break;
            case Collectables.Type.Food:
            case Collectables.Type.Drink:
            case Collectables.Type.Medical:
                
                break;
            case Collectables.Type.Other:
                break;
        }

        return used;
    }

    private bool Consume(Collectables item)
    {
        if(eat + item.calories > 3000 || drink + item.liquid > 3000)
        {
            StartPopUpMessage.Message("You're way too full to consume this item!",Color.yellow);
            return false;
        }
        else 
        {
            // SOON => CHECK DISEASES
            drink += item.liquid;
            eat += item.calories;
            return true;
        }
    }


    public void UpdateStatus()
    {
        if(cold > 0 || Enviroment.Degree > 0)
        {
            cold += Enviroment.Degree * Time.deltaTime / 15;
        }
        else
        {
            cold = 0;
            health -= -1 * Enviroment.degree * Time.deltaTime / 25;
        }

        if(eat > 0)
        {
            eat -= Time.deltaTime * 4;
        } else
        {
            eat = 0;
            health -= Time.deltaTime * 4;
        }

        if(drink > 0)
        {
            drink -= Time.deltaTime * 4;
        } else
        {
            drink = 0;
            health -= Time.deltaTime * 4;
        }

        Check();

        healthBar.fillAmount = health / 100;
        warmthBar.fillAmount = cold / 100;
        hungerBar.fillAmount = eat / 3000;
        thirstBar.fillAmount = drink / 3000;
    }

    private void Check()
    {
        if (drink > 3000)
            drink = 3000;
        if (cold > 100)
            cold = 100;
        if (health > 100)
            health = 10;
        if (eat > 3000)
            eat = 3000;
    }

    private void Update()
    {
        UpdateStatus();
    }
}
