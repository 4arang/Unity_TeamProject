using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    [SerializeField]
    private GameObject champInfoContainer;

    [SerializeField]
    private List<GameObject> abilityInfoSlots;

    [SerializeField]
    private List<Image> skillImage;

    private int selectedChamp;
    private void Start()
    {
        champInfoContainer.SetActive(false);
        selectedChamp = -1;
    }

    public void OnChampInfoButtonClicked(int whichChamp)
    {
        champInfoContainer.SetActive(true);
        selectedChamp = whichChamp;

        UpdateAbilityInfo();
    }

    private void UpdateAbilityInfo()
    {
        switch(selectedChamp)
        {
            case 0: //Baek
                {
                    for(int i=0;i< skillImage.Count;i++)
                    {
                        skillImage[i].sprite = GameDataSource.Instance.m_BaekRangSkillData[i].Icon;
                        abilityInfoSlots[i].GetComponent<UISlot>().abilityData = GameDataSource.Instance.m_BaekRangSkillData[i];
                    }
                    break;
                }
            case 1: //ColD
                {
                    for (int i = 0; i < skillImage.Count; i++)
                    {
                        skillImage[i].sprite = GameDataSource.Instance.m_ColDSkillData[i].Icon;
                        abilityInfoSlots[i].GetComponent<UISlot>().abilityData = GameDataSource.Instance.m_ColDSkillData[i];
                    }
                    break;
                }
            case 2: //Xerion
                {
                    for (int i = 0; i < skillImage.Count; i++)
                    {
                        skillImage[i].sprite = GameDataSource.Instance.m_XerionSkillData[i].Icon;
                        abilityInfoSlots[i].GetComponent<UISlot>().abilityData = GameDataSource.Instance.m_XerionSkillData[i];

                    }
                    break;
                }
        }

    }
}
