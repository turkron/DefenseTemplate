using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siteInventory : MonoBehaviour
{

    public GameObject builtItem = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buildItem (GameObject item)
    {
        builtItem = item;
    }
}
