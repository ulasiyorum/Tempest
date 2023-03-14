using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum Type
    {
        Food,
        Drink,
        Burn,
        Cloth,
        Medical,
        Other
    }
    [SerializeField] GameObject equipMenu;

    public string itemName;
    public Type type;
    public float condition = 100;

    public float calories = 0;
    public float liquid = 0;


    public float burnDuration = 0;
    public float burnDegree = 0;

    public float clothDegree = 0;

    public float itemAmount = 0;
    public float itemWeight = 0;

    public float sicknessChance = 0;

    private void Start()
    {

    }

    private void Update()
    {
        if (condition > 0)
            condition -= Time.deltaTime / 1000;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        equipMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        equipMenu.SetActive(true);
    }
}
