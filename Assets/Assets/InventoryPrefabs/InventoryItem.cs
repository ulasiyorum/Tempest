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
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
