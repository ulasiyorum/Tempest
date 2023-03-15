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

    private bool buttonClicked; // This is so we can await a response from user when starting a fire
    private bool fire; // This is so we can know what response they gave us when asking them to start the fire

    public const int MaxCarry = 40;

    private List<Collectables> inventory;


    [SerializeField] Image healthBar;
    [SerializeField] Image warmthBar;
    [SerializeField] Image hungerBar;
    [SerializeField] Image thirstBar;
    [SerializeField] GameObject startFire;


    private void Start()
    {
        buttonClicked = false;
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

        return used;
    }

    private bool StartAFire(Collectables item)
    {
        bool used = false;
        startFire.SetActive(true);
        startFire.transform.position = transform.position + new Vector3(3f,0f);
        StartCoroutine(WaitUI((bool result) => {
            used = result;
        }));
        buttonClicked = false;

        if(fire)
        {
            GameObject go = Instantiate(AssetsHandler.i.firePrefab, GameManager.i.canvas.transform, true);
            go.transform.position = startFire.transform.position;
            Enviroment.firePosition = go.transform.position;
            Enviroment.fireDegree = item.burnDegree;
            Enviroment.fireDuration = item.burnDuration;
        }

        startFire.SetActive(false);

        return used;
    }

    public void Choose(bool start)
    {
        fire = start;
        buttonClicked = true;
    }

    private IEnumerator WaitUI(Action<bool> result)
    {
        yield return new WaitUntil(() => buttonClicked);

        result.Invoke(fire);
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
