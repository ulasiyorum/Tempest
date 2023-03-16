using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireOnClick : MonoBehaviour, IPointerDownHandler
{
    private Inventory inventoryReference;
    private GameObject fireManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        fireManager.SetActive(true);
        inventoryReference.GetInventoryBurnables();
    }

    void Start()
    {
        inventoryReference = GameManager.i.Inventory;
        fireManager = GameManager.i.fireManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
