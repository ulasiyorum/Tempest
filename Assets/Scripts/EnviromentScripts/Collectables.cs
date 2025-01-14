using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Sprite icon;

    public enum Type
    {
        Food,
        Drink,
        Burn,
        Cloth,
        Medical,
        Tool,
        Other
    }



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

    public float ToolAttackDamage = 0;
    public float ToolSpeed = 0;

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
        EquipMenu.instance.anim.Play("FadeOut");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EquipMenu.instance.gameObject.activeInHierarchy)
            return;
        EquipMenu.instance.gameObject.SetActive(true);
        EquipMenu.instance.anim.Play("FadeIn");
        EquipMenu.instance.ChangeMenu(this);
    }
}
