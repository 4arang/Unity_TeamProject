using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UITooltipDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    LobbyController lobbyController;
    [SerializeField]
    [Tooltip("The actual Tooltip that should be triggered")]
    private UITooltipPopup m_TooltipPopup;

    [SerializeField]
    [Multiline]
    [Tooltip("The text of the tooltip (this is the default text; it can also be changed in code)")]
    private string m_TooltipText;

    [SerializeField]
    [Tooltip("Should the tooltip appear instantly if the player clicks this UI element?")]
    private bool m_ActivateOnClick = true;

    // This delay-time could be a member variable, but it's annoying to have to change it on
    // every tooltip spot everywhere! So for now we just hard-code it.
    private const float k_TooltipDelay = 0.5f;

    private float m_PointerEnterTime = 0;
    public bool m_IsShowingTooltip=false;
    public void SetText(string text)
    {
        bool wasChanged = text != m_TooltipText;
        Debug.Log($"was changed{wasChanged}");
        m_TooltipText = text;
        Debug.Log($"m_TooltipText={m_TooltipText}");
        if (wasChanged && m_IsShowingTooltip)
        {
            // we changed the text while of our tooltip was being shown! We need to re-show the tooltip!
            HideTooltip();
            ShowTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_PointerEnterTime = Time.time;
        Debug.Log("TooltipActive");
        AbilityData ability = GetComponent<UISlot>().abilityData;

        m_TooltipPopup.SetupTooltip(ability.name, ability.Description,ability.Icon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("TooltipUnActive");
        m_PointerEnterTime = 0;
        HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_ActivateOnClick)
        {
            Debug.Log("OnPointer Clicked");
            ShowTooltip();
        }
    }

    private void Update()
    {
        if (m_PointerEnterTime != 0 && (Time.time - m_PointerEnterTime) > k_TooltipDelay)
        {
            ShowTooltip();
        }
    }

    private void ShowTooltip()
    {
        if (!m_IsShowingTooltip)
        {
            m_TooltipPopup.ShowTooltip(m_TooltipText, Input.mousePosition);
            m_IsShowingTooltip = true;            
        }
    }

    private void HideTooltip()
    {
        if (m_IsShowingTooltip)
        {
            m_TooltipPopup.HideTooltip();
            m_IsShowingTooltip = false;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (gameObject.scene.rootCount > 1) // Hacky way for checking if this is a scene object or a prefab instance and not a prefab definition.
        {
            if (!m_TooltipPopup)
            {
                // typically there's only one tooltip popup in the scene, so pick that
                m_TooltipPopup = FindObjectOfType<UITooltipPopup>();
            }
        }
    }
#endif
}

