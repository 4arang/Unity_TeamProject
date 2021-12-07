using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Bar : MonoBehaviour
{
    [SerializeField] private Image imgCoolDown;
    [SerializeField] private Text txtCoolDown;

    private bool bCoolDown = false;
    private float coolDownTime;
    private float coolDownTimer = 0.0f;


    void Start()
    {
        txtCoolDown.gameObject.SetActive(false);
        imgCoolDown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(bCoolDown)
        {
            coolDown();
        }
        
    }

    public void OnSkill(float time)
    {
        coolDownTime = time;
        UseSpell();
    }

    void coolDown()
    {
        coolDownTimer -= Time.deltaTime;

        if(coolDownTimer<0.0f)
        {
            bCoolDown = false;
            txtCoolDown.gameObject.SetActive(false);
            imgCoolDown.fillAmount = 0.0f;
        }
        else
        {
            txtCoolDown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imgCoolDown.fillAmount = coolDownTimer / coolDownTime;
        }
    }

    public void UseSpell()
    {
        if (!bCoolDown)
        {
            bCoolDown = true;
            txtCoolDown.gameObject.SetActive(true);
            coolDownTimer = coolDownTime;
        }
    }
}
