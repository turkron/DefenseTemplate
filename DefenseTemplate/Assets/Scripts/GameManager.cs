using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] Mobs;
    public GameObject[] SpawnedMobs;
    public List<GameObject> PlayerMobs = new List<GameObject>();
    public GameObject[] Waves;
    public GameObject SpawnLocation;
    public GameObject PauseScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject MainGameScreen;
    private int currency = 100;
    public int Currency { get => currency; }
    public void DeductCurrency(int value) { currency -= value; }
    public string GameMode { get => gameMode; set => gameMode = value; }
    public float BuildTimeDuration { get => buildTimeDuration; set => buildTimeDuration = value; }

    public float inspectorBuildDuration = 30f;
    private float buildTimeDuration = 30f;
    private string gameMode = "introPhase";
    private bool gameIsPaused = false;
    public float introTimer = 5.0f;
    private int currentWave = 0;
    public int CurrentWave { get => currentWave; }
    private string waveInfo = "Na";
    public string WaveInfo { get => waveInfo; }


    private void showUIComponent(GameObject UIElement, bool show)
    {
        UIElement.SetActive(show);
    }
    public string getGameMode()
    {
        return gameMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnLocation = GameObject.FindGameObjectWithTag("SpawnLocation");
    }

    // Update is called once per frame
    void Update()
    {
        handlePause();
        switch (gameMode)
        {
            //when the game starts, allow for 5 seconds to do introduction to level.
            case "introPhase":
                handleIntro();
                break;
            //then allow player to do first build mode. 
            case "startBuildPhase":
                startBuildPhase();
                break;
            case "buildPhase":
                //then start to count down a timer.
                handleBuildPhase();
                break;
            case "startActionPhase":
                //spawn mobs and switch controls
                startActionPhase();
                break;
            case "actionPhase":
                //track mobs
                handleActionPhase();
                break;
            case "isComplete":
                handleGameEnd();
                break;
        }
    }

    void handlePause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale != 0)
            {
                gameIsPaused = true;
                Time.timeScale = 0;
                Debug.Log("Game is paused");
            }
            else
            {
                gameIsPaused = false;
                Time.timeScale = 1;
                Debug.Log("Game is resuming");
            }
            showUIComponent(PauseScreen, gameIsPaused);
        }
    }
    void handleIntro()
    {
        if (introTimer > 0.0f)
        {
            introTimer -= Time.deltaTime;
            return;
        }
        if(PlayerMobs.Count == 0)
        {
            Debug.Log("please build your starting item");
            return;
        }
        gameMode = "startBuildPhase";
        Debug.Log(gameMode);
    }
    void startBuildPhase()
    {
        //initialise buildPhase;
        //switch control system to build
        //reset buildTimer;
        //switch game to buildPhase;
        buildTimeDuration = inspectorBuildDuration;
        gameMode = "buildPhase";
        Debug.Log(gameMode);
    }
    void handleBuildPhase()
    {
        //start countDown
        if (buildTimeDuration > 0f) { buildTimeDuration -= Time.deltaTime; } else { gameMode = "startActionPhase"; }
    }
    void startActionPhase()
    {
        //initialise action phase;
        //increase currentWave;
        //spawn "wave" mobs at points.
        //switch gameMode to actionPhase
        Instantiate(Waves[currentWave], SpawnLocation.transform.position, Quaternion.identity);
        currentWave++;
        gameMode = "actionPhase";
        Debug.Log(gameMode);
    }
    void handleActionPhase()
    {
        //track current amount of mobs on screen. 
        //if some still alive
        //return
        //else
        //start buildPhase.
        PlayerMobs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")) ;
        SpawnedMobs = GameObject.FindGameObjectsWithTag("Hostile");
        if (SpawnedMobs.Length != 0)
        {
            return;
        }
        else if (PlayerMobs.Count == 0 || currentWave <= Waves.Length)
        {
            Debug.Log("startGameOver");
            gameMode = "isComplete";
        } 
        else
        {
            Debug.Log("startingBuildPhase");
            gameMode = "startBuildPhase";
        }
    }
    void handleGameEnd()
    {
        //get the health of the player and see if its less or equal to 0;
        //return correct game screen;
        bool winState = PlayerMobs.Count == 0;
        showUIComponent(LoseScreen, !winState);
        showUIComponent(WinScreen, winState);
    }
}


