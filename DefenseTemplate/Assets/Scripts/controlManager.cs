using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlManager : MonoBehaviour
{

    private string controlType = "null";
    void setGameMode(string gameMode)
    {
        if(gameMode == "startBuildPhase")
        {
            controlType = "buildMode";
        } else if(gameMode == "startActionPhase")
        {
            controlType = "actionMode";
        }
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
