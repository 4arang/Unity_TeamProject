using UnityEngine;
using UnityEngine.UI;
public class UISlot : MonoBehaviour
{
    UITooltipDetector tooltipDetector;
    [SerializeField]
    private int slotIdx;
    //public Image slotImage;
    int currentIdx;
    public enum SlotType
    {
        Item,
        Ability,
        Champion,
        SummonerSpell
    }
    public SlotType slotType;

    public AbilityData abilityData=null;
    public string abilityDescription;
    public string abilityName;
    public Image abilityImage;
    private void Start()
    {
        abilityDescription = abilityData.Description;
        abilityName = abilityData.DisplayedName;
    }
    private void LateUpdate()
    {
        abilityDescription = abilityData.Description;
        abilityName = abilityData.DisplayedName;
    }
        
}
