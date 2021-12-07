using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_BarQ : MonoBehaviour
{
    [SerializeField] private Image imgCoolDown;
    [SerializeField] private Text txtCoolDown;

    private bool bCoolDown = false;
    private float coolDownTime;
    private float coolDownTimer = 0.0f;


    void Start()
    {
        txtCoolDown.gameObject.SetActive(false); //숫자표시
        imgCoolDown.fillAmount = 0.0f;  //쿨타임이미지
    }

    // Update is called once per frame
    void Update()
    {
        if (bCoolDown)
        {
            coolDown();
        }
       
    }

    public void OnSkill(float time) //스킬별 쿨타임시간
    {
        coolDownTime = time;    //시간지정
        UseSpell();
    }

    void coolDown()
    {
        coolDownTimer -= Time.deltaTime; //시간--

        if (coolDownTimer < 0.0f)
        {
            bCoolDown = false; //쿨타임종료
            txtCoolDown.gameObject.SetActive(false); //숫자표시x
            imgCoolDown.fillAmount = 0.0f; //쿨타임이미지 x
        }
        else
        {
            txtCoolDown.text = Mathf.RoundToInt(coolDownTimer).ToString(); //라운딩
            imgCoolDown.fillAmount = coolDownTimer / coolDownTime; //쿨타임이미지
        }
    }

    public void UseSpell()
    {
        if (!bCoolDown) 
        {
            bCoolDown = true;
            txtCoolDown.gameObject.SetActive(true); //숫자표시
            coolDownTimer = coolDownTime;   //쿨타임 적용
        }
    }
}
