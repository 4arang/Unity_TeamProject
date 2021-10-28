using UnityEngine;
using TMPro;
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI valueTxt;

    public void SetupTooltip(string name,string des, int value)
    {
        nameTxt.text = name;
        descriptionTxt.text = des;

        valueTxt.text = value.ToString();
    }
}
