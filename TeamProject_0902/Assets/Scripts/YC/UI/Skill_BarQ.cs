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
        txtCoolDown.gameObject.SetActive(false); //����ǥ��
        imgCoolDown.fillAmount = 0.0f;  //��Ÿ���̹���
    }

    // Update is called once per frame
    void Update()
    {
        if (bCoolDown)
        {
            coolDown();
        }
       
    }

    public void OnSkill(float time) //��ų�� ��Ÿ�ӽð�
    {
        coolDownTime = time;    //�ð�����
        UseSpell();
    }

    void coolDown()
    {
        coolDownTimer -= Time.deltaTime; //�ð�--

        if (coolDownTimer < 0.0f)
        {
            bCoolDown = false; //��Ÿ������
            txtCoolDown.gameObject.SetActive(false); //����ǥ��x
            imgCoolDown.fillAmount = 0.0f; //��Ÿ���̹��� x
        }
        else
        {
            txtCoolDown.text = Mathf.RoundToInt(coolDownTimer).ToString(); //����
            imgCoolDown.fillAmount = coolDownTimer / coolDownTime; //��Ÿ���̹���
        }
    }

    public void UseSpell()
    {
        if (!bCoolDown) 
        {
            bCoolDown = true;
            txtCoolDown.gameObject.SetActive(true); //����ǥ��
            coolDownTimer = coolDownTime;   //��Ÿ�� ����
        }
    }
}
