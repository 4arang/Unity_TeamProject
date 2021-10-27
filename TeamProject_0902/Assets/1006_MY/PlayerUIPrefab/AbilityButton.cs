using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    public ActionDatabase abilityData;

    public void Print()
    {
        Debug.Log(abilityData.Description);
        Debug.Log(abilityData.ActionTypeEnum);
    }
}
