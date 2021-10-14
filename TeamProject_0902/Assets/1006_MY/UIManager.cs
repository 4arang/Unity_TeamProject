using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

   // public ChampionStats CurrentPlayer;

    public Text TimeText;

    //public float gameTime;
    //public Text timeText;

    public Text FPSText;
    public Text MSText;
    float deltaTime = 0.0f; //Check FPS

    public Text RedKills;
    public Text BlueKills;    

    public Text PlayerScore;
    public Text PlayerMinionScore;

    public Text PlayerHealth;
    public Text PlayerResource;
    public Text PlayerLevel;
    public Text PlayerGold;

    public Image PlayerHealthImage;
    public Image PlayerResourceImage;
    public Image PlayerExp;

    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    private void Start()
    {
        //CurrentPlayer = InGameManager.Instance.Teams[InGameManager.Instance.CurrentPlayerID].Champions[0];
    }
    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        MSText.text = (deltaTime * 1000.0f).ToString("0.0" + "ms");
        FPSText.text = (1.0f / deltaTime).ToString("0." + "fps");
        TimeText.text = GameManager.Instance.GameTime.ToString("00:00");

        //RedKills.text = GameManager.GetTeamKills(0).ToString();
        //BlueKills.text = GameManager.GetTeamKills(1).ToString();
        //PlayerScore.text = string.Format("{0}/{1}/{2}",
        //    CurrentPlayer.Kills,
        //    CurrentPlayer.Deaths,
        //    CurrentPlayer.Assists);
        //PlayerMinionScore.text = CurrentPlayer.MinionScore.ToString();

        //PlayerHealth.text = CurrentPlayer.health.ToString();
        //PlayerResource.text = CurrentPlayer.resource.ToString();
        //PlayerLevel.text = CurrentPlayer.level.ToString();
        //PlayerGold.text = CurrentPlayer.gold.ToString();

        //PlayerHealthImage.fillAmount = CurrentPlayer.health;
        //PlayerResourceImage.fillAmount = CurrentPlayer.resource;
        //PlayerExp.fillAmount = CurrentPlayer.expValue;
        if(Input.GetKey(KeyCode.Tab))
        {
            StartCoroutine(PopUpScoreBoard());
        }        
    }

    IEnumerator PopUpScoreBoard()
    {
        return null;
    }
}
