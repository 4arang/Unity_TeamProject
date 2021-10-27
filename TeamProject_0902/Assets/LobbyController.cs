using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private GameObject champInfoContainer;

    [SerializeField]
    private List<UITooltipDetector> champAbilities;
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
        foreach (var skill in champAbilities)
        {
            Image img=skill.GetComponent<Image>();
            GameDataSource.Instance.
        }

    }
}
