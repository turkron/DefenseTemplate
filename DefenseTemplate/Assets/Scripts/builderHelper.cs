using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class builderHelper : MonoBehaviour
{
    public GameObject PrefabToBuild;
    public int currencyValue;
    private GameManager gameManager;
    public bool active = false;
    private Material m_Material;
    private GameObject parent;
    private siteInventory inventory;
    private GameObject buildSite;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        parent = transform.parent.gameObject;
        inventory = parent.GetComponent<siteInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        active = checkCurrency();
        if (!active)
        {
            m_Material.color = Color.black;
        }
    }

    bool checkCurrency() //function used to check to see if the player has enough currency, might have to be called every frame.
    {
        return gameManager.Currency > currencyValue;
    }
    public void BuildItem(GameObject parentBuildSite)
    {
        if (inventory.builtItem == null)
        {
            DeductCurrency();
            inventory.builtItem = Instantiate(PrefabToBuild, parent.transform.position, parent.transform.rotation);
            buildSite = parentBuildSite = inventory.builtItem.GetComponent<playerManager>().parentBuildSite;
            gameManager.PlayerMobs.Add(inventory.builtItem);
        } else
        {
            Debug.Log("why are we building again when there is already something here?");
        }
        
    } 
    void DeductCurrency()
    {
        gameManager.DeductCurrency(currencyValue);
    }
    void OnMouseOver()
    {
        if (active)
        {
            // Change the Color of the GameObject when the mouse hovers over it
            m_Material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        if (active)
        {
            //Change the Color back to blue when the mouse exits the GameObject
            m_Material.color = Color.black;
        }
    }


}

