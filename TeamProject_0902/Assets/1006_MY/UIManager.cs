using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private UISetting uiSetting;

    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    private void Start()
    {
        uiSetting = GetComponent<UISetting>();
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
        if (Input.GetKeyDown(KeyCode.Tab))
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
        uiSetting.scoreBoardPanel.gameObject.SetActive(true);
        
        yield return null;
    }
    IEnumerator CloseScoreBoard()
    {
        uiSetting.scoreBoardPanel.gameObject.SetActive(false);

        yield return null;
    }
}
