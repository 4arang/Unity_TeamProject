using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Lvlup_Button : MonoBehaviour
{
    private Transform player;
    private int skillNum;
    [SerializeField] GameObject lvlupUI;
    private int level = 1;

    private void Start()
    {
        player = FindObjectOfType<MapCamera>().PlayerToMove;
        if (transform.position.x <= 850)
        {
            skillNum = 1; //skill Q
            return;
        }
        else if (transform.position.x <= 900)
        {
            skillNum = 2; //skill W
            return;
        }
        else if (transform.position.x <= 950)
        {
            skillNum = 3; //skill E
            return;
        }
        else
            skillNum = 4; //skill R
    }

    public void onLevelUp()
    {
        switch (skillNum)
        {
            case 1:
                player.GetComponent<Player_Stats>().LevelupQ();
                break;
            case 2:
                player.GetComponent<Player_Stats>().LevelupW();
                break;
            case 3:
                player.GetComponent<Player_Stats>().LevelupE();
                break;
            case 4:
                player.GetComponent<Player_Stats>().LevelupR();
                break;
        }
        level++;
        StartCoroutine("ShowClickedImg");
    }

    IEnumerator ShowClickedImg()
    {
       yield return new WaitForSeconds(0.5f);
        lvlupUI.SetActive(false);
    }

    private void OnEnable()
    {
        if(skillNum ==4)
        {
            if (level >= 3) gameObject.SetActive(false);
        }
        else
        {
            if (level >= 5) gameObject.SetActive(false);
        }
    }
}
