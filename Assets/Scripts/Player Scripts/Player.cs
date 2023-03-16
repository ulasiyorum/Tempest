using System;
using System.Collections;
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

    public static bool consuming = false;
    private float drinkConsume = 0;
    private float eatConsume = 0;


    private Collectables temporaryFireCol;

    [SerializeField] Image healthBar;
    [SerializeField] Image warmthBar;
    [SerializeField] Image hungerBar;
    [SerializeField] Image thirstBar;
    [SerializeField] GameObject startFire;


    private void Start()
    {
        health = 100;
        cold = 100;
        eat = 3000;
        drink = 1500;
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
                StartAFire(item);
                break;
            case Collectables.Type.Cloth:
                Debug.Log("Implement wearing cloth");
                break;
            case Collectables.Type.Food:
            case Collectables.Type.Drink:
            case Collectables.Type.Medical:
                used = Consume(item);
                break;
            case Collectables.Type.Other:
                Debug.Log("Implement other");
                break;
            default:
                Debug.Log("Implement default");
                break;
        }

        if(used)
        {
            inventory.Remove(item);
        }

        return used;
    }

    private bool StartAFire(Collectables item)
    {
        startFire.SetActive(true);
        startFire.transform.position = transform.position + new Vector3(3f,0f);
        temporaryFireCol = item;
        return false;
    }

    public void Choose(bool start)
    {
        if(start)
        {
            GameObject go = Instantiate(AssetsHandler.i.firePrefab, GameManager.i.canvas.transform, false);
            go.transform.position = startFire.transform.position;
            Enviroment.firePosition = go.transform.position;
            Enviroment.fireDegree = temporaryFireCol.burnDegree;
            Enviroment.fireDuration = temporaryFireCol.burnDuration;

            inventory.Remove(temporaryFireCol);
        }
    }


    private bool Consume(Collectables item)
    {
        consuming = true;
        //Play anim & sound
        eatConsume += item.calories;
        drinkConsume += item.liquid;

        return true;
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
            eat -= Time.deltaTime * 10;
        } else
        {
            eat = 0;
            health -= Time.deltaTime * 10;
        }

        if(drink > 0)
        {
            drink -= Time.deltaTime * 8;
        } else
        {
            drink = 0;
            health -= Time.deltaTime * 8;
        }

        Check();

        healthBar.fillAmount = health / 100;
        warmthBar.fillAmount = cold / 100;
        hungerBar.fillAmount = eat / 3000;
        thirstBar.fillAmount = drink / 1500;
    }

    private void Check()
    {
        if (drink > 1500)
            drink = 1500;
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

        if(consuming)
        {
            if(drinkConsume > 0 && drink <= 1500)
            {
                if (drinkConsume >= Time.deltaTime * 200)
                {
                    drink += Time.deltaTime * 200;
                    drinkConsume -= Time.deltaTime * 200;
                } else
                {
                    drink += drinkConsume;
                    drinkConsume -= drinkConsume;
                }
            }
            else if(drinkConsume < 0 && drink > 0)
            {
                if (drinkConsume <= Time.deltaTime * 200)
                {
                    drink -= Time.deltaTime * 200;
                    drinkConsume += Time.deltaTime * 200;
                } else
                {
                    drink -= drinkConsume;
                    drinkConsume += drinkConsume;
                }
            }
            if(eatConsume > 0 && eat <= 3000)
            {
                if (eatConsume >= Time.deltaTime * 200)
                {
                    eat += Time.deltaTime * 200;
                    eatConsume -= Time.deltaTime * 200;
                } else
                {
                    eat += eatConsume;
                    eatConsume -= eatConsume;
                }
            }

            if ((eatConsume == 0 && drinkConsume == 0) || (eat == 3000 && drink == 1500))
            {
                eatConsume = 0;
                drinkConsume = 0;
                consuming = false;
            }
        }
    }
}
