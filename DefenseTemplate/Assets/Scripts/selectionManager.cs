using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionManager : MonoBehaviour
{
    public Camera camera;
    public GameObject selectedObject;
    public GameObject buildUI;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           selectBuildSite();
        };
    }

    void selectBuildSite()
    {
        
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "BuildLocation")
            {
                selectedObject = hit.transform.gameObject;
                buildUI.transform.position = selectedObject.transform.position;
                buildUI.active = true;
                return;
            }
            if(hit.transform.tag == "BuildUI")
            {
                if(hit.transform.parent.GetComponent<siteInventory>().builtItem == null)
                {
                    if (hit.transform.gameObject.GetComponent<builderHelper>().active)
                    {
                        hit.transform.gameObject.GetComponent<builderHelper>().BuildItem(selectedObject);
                        selectedObject.active = false;
                        selectedObject = null;
                        buildUI.active = false;
                    }
                }
                return;
            }
        }
        
        selectedObject = null;
        buildUI.active = false;
        return;
    }
}
