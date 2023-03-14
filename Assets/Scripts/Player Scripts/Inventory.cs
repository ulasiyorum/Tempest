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
        GetInventory();
    }

    public void GetInventory()
    {
        Player player = GameManager.i.player;
        Collectables[] col = player.Inventory.ToArray();
        GameObject itemPrefab = AssetsHandler.i.inventoryItemPrefab;
        foreach (Collectables collectable in col)
        {
            GameObject go = Instantiate(itemPrefab,parent);
            InventoryItem item = go.GetComponent<InventoryItem>();
            item._name.text = collectable.name;
            item._icon.sprite = collectable.icon;
            generated.Add(go);
        }
    }

    public void CloseInventory()
    {
        foreach(GameObject go in generated)
        {
            Destroy(go);
        }
        generated.Clear();
    }
}
