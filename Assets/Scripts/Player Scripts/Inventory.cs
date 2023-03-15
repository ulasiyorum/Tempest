using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] Transform parent;
    private List<GameObject> generated;
    private void Start()
    {

    }

    public void GetInventory()
    {
        generated = new List<GameObject>();
        Player player = GameManager.i.player;
        List<Collectables> col = player.Inventory;
        GameObject itemPrefab = AssetsHandler.i.inventoryItemPrefab;
        foreach (Collectables collectable in col)
        {
            GameObject go = Instantiate(itemPrefab,parent);
            InventoryItem item = go.GetComponent<InventoryItem>();
            item._name.text = collectable.itemName;
            item._icon.sprite = collectable.icon;
            item.collectable = collectable;
            generated.Add(go);
        }
    }


    public void CloseInventory()
    {
        foreach(GameObject go in generated)
        {
            Destroy(go);
        }
    }
}
