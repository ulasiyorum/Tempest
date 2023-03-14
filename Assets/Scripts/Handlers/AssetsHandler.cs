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
}
