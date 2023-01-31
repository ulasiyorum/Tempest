using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsHandler : MonoBehaviour
{
    private static AssetsHandler instance;
    public static AssetsHandler Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AssetsHandler>();
            
            return instance;
        }
    }


    [SerializeField] GameObject[] mapPrefabs;
    [SerializeField] Sprite[] collectableAssets;

    public GameObject collectablePrefab;
    public GameObject[] MapPrefabs { get => mapPrefabs; }
    public Sprite[] CollectableAssets { get => collectableAssets; }
}
