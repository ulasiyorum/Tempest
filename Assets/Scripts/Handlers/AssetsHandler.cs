using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsHandler : MonoBehaviour
{
    public static AssetsHandler i;
    public GameObject[] colPrefabs;
    public GameObject[] mapPrefabs;
    public GameObject popUpPrefab;
    public GameObject inventoryItemPrefab;
    public GameObject firePrefab;
    public GameObject inventoryItemForFirePrefab;
    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject FindPrefab(string name)
    {

        foreach(var prefab in colPrefabs)
        {
            if(prefab.GetComponent<Collectables>().itemName == name)
            {
                return prefab;
            }
        }

        return null;
    }
}
