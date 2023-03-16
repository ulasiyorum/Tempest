using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireOnClick : MonoBehaviour, IPointerDownHandler
{
    private Inventory inventoryReference;
    private GameObject fireManager;

    [SerializeField] TMP_Text degreeText;
    [SerializeField] TMP_Text secondsText;

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

        secondsText.text = (int)Enviroment.fireDuration + " seconds left";
        degreeText.text = Enviroment.fireDegree + " degrees";

        if (Enviroment.fireDuration <= 0)
            Destroy(gameObject);

    }
}
