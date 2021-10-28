using UnityEngine;
using UnityEngine.UI;
public class UISlot : MonoBehaviour
{    
    [SerializeField]
    private int slotIdx;

    int currentIdx;
    public enum SlotType
    {
        Item,
        Ability,
        Champion
    }
    public SlotType slotType;

    public AbilityData abilityData=null;
    public string abilityDescription;
    public string abilityName;
    public Image abilityImage; 
    private void LateUpdate()
    {
        abilityDescription = abilityData.Description;
        abilityName = abilityData.DisplayedName;
        abilityImage.sprite = abilityData.Icon;
    }
}
