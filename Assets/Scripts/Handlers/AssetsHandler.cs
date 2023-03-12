using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsHandler : MonoBehaviour
{
    public static AssetsHandler i;

    public GameObject[] mapPrefabs;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
