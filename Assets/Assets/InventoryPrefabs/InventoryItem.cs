using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Collectables collectable;
    public Image _icon;
    public TMP_Text _name;

    public void DropItem()
    {
        GameObject go = Instantiate(AssetsHandler.i.FindPrefab(collectable.itemName),GameManager.i.canvas.transform,true);
        go.transform.position = GameManager.i.player.transform.position;
        GameManager.i.player.Drop(collectable);
        Destroy(gameObject);
    }

    public void UseItem()
    {
        bool used = GameManager.i.player.UseItem(collectable);
        if (used)
            Destroy(gameObject);
    }

    public void UseFire()
    {
        Enviroment.fireDegree += collectable.burnDegree;
        Enviroment.fireDuration += collectable.burnDuration;
        GameManager.i.player.Drop(collectable);
    }
}
