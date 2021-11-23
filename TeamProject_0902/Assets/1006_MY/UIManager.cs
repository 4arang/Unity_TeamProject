using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager sInstance;
    public static UIManager Instance
    {
        get
        {
            if (sInstance == null)
             {
                 GameObject newGameObj = new GameObject("UIManager");
                 sInstance = newGameObj.AddComponent<UIManager>();
             }
             return sInstance;
        }
    }


    public float Exp;// exp to level up : 280 + (level-1)*100
    public int Gold;
    public int Level; //max 18

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(PopUpScoreBoard());
        }
        else
        {
            StartCoroutine(CloseScoreBoard());
        }
    }

    IEnumerator PopUpScoreBoard()
    {
        PlayerUI playerUI = GetComponent<PlayerUI>();
        playerUI.scoreBoardPanel.gameObject.SetActive(true); ;

        yield return null;
    }
    IEnumerator CloseScoreBoard()
    {
        PlayerUI playerUI = GetComponent<PlayerUI>();
      //  playerUI.scoreBoardPanel.gameObject.SetActive(false); ;
        yield return null;

    }
}
