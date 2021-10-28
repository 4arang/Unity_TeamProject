using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class AbilityMaskDisplay : MonoBehaviour
{
    int champIdx;
    public AbilityData[] ability1;    

    public Image abilityImage;
    //public Text toolTip;

    private void Start()
    {
        champIdx = PlayerInfo.PI.mySelectedChampion;
        abilityImage.sprite = ability1[champIdx].Icon;
    }
}
