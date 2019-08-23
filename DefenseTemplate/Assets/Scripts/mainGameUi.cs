using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainGameUi : MonoBehaviour
{
    public Text Money;
    public Text CurrentWave;
    public Text WaveInfo;
    public Text GameMode;
    public Text MiscInfo;
    public GameManager gameManager;
        
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        Money = GameObject.Find("Money").GetComponent<Text>();
        CurrentWave = GameObject.Find("CurrentWave").GetComponent<Text>();
        WaveInfo = GameObject.Find("WaveInfo").GetComponent<Text>();
        GameMode = GameObject.Find("GameMode").GetComponent<Text>();
        MiscInfo = GameObject.Find("MiscInfo").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Money.text = "Money: " + gameManager.Currency;
        CurrentWave.text = "Current Wave: " + gameManager.CurrentWave;
        WaveInfo.text = "Wave Info: " + gameManager.WaveInfo;
        GameMode.text = "GameMode: " + gameManager.GameMode;
    }

    IEnumerator FlashMiscInfo(string Message)
    {
       yield return StartCoroutine (FlashMiscInfo(Message, 5f));
    }

    IEnumerator FlashMiscInfo(string Message, float FlashDuration)
    {
        MiscInfo.text = Message;
        yield return new WaitForSeconds(FlashDuration);
        MiscInfo.text = " ";
    }
}
