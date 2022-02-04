using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UI_Bar : MonoBehaviour
{
    [SerializeField] private Slider UIbarHP;
    [SerializeField] private Slider UIbarRP;
    private Text hptext;
    private Text rptext;

    StringBuilder HPsb;
    StringBuilder RPsb;

    void Awake()
    {
       // UIbar = GetComponentsInChildren<Slider>();
        hptext = UIbarHP.gameObject.GetComponentInChildren<Text>();
       rptext = UIbarRP.gameObject.GetComponentInChildren<Text>();

       // HPsb = new StringBuilder("");
      //  RPsb = new StringBuilder("");
    }

    public void SetMaxHP(float maxHP) 
    {
        UIbarHP.maxValue = maxHP;
    }

    public void SetHP(float hp)
    {
        UIbarHP.value = hp;
        SetHPtext();
    }

    public void SetMaxRP(float maxRP)
    {
        UIbarRP.maxValue = maxRP;
    }

    public void SetRP(float rp)
    {
        UIbarRP.value = rp;
        SetRPtext();
    }

    private void SetHPtext()
    {

        HPsb = new StringBuilder("");
        HPsb.Clear();
        HPsb.Append(UIbarHP.value);
        HPsb.Append("  /  ");
        HPsb.Append(UIbarHP.maxValue);

        hptext.text = HPsb.ToString();
    }

    private void SetRPtext()
    {

        RPsb = new StringBuilder("");
        RPsb.Clear();
        RPsb.Append(UIbarRP.value);
        RPsb.Append("  /  ");
        RPsb.Append(UIbarRP.maxValue);

       rptext.text = RPsb.ToString();
    }
}
