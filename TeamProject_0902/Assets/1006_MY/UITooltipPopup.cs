using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This controls the tooltip popup -- the little text blurb that appears when you hover your mouse
/// over an ability icon.
/// </summary>
public class UITooltipPopup : MonoBehaviour
{
    [SerializeField]
    private Canvas m_Canvas;
    [SerializeField]
    [Tooltip("This transform is shown/hidden to show/hide the popup box")]
    private GameObject m_WindowRoot;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI valueTxt;
    public Image Icon;

    [SerializeField]
    private TextMeshProUGUI m_TextField;
    [SerializeField]
    private Vector3 m_CursorOffset;

    float halfwidth;
    RectTransform rt;
    private void Start()
    {
        //halfwidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        //rt = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //transform.position = Input.mousePosition;

        //if (rt.anchoredPosition.x + rt.sizeDelta.x > halfwidth)
        //    rt.pivot = new Vector2(1, 1);
        //else
        //    rt.pivot = new Vector2(0, 1);
    }
    /// <summary>
    /// Show the current tooltip.
    /// </summary>
    public void ShowTooltip(string text, Vector2 mousePos)
    {
        m_WindowRoot.SetActive(true);

    }
    /// <summary>
    /// Hides the current tooltip.
    /// </summary>
    public void HideTooltip()
    {
        m_WindowRoot.SetActive(false);
    }
    public void SetupTooltip(string name, string des, Sprite icon)
    {
        Debug.Log("툴팁 업데이트 완료");
        nameTxt.text = name;
        descriptionTxt.text = des;
        Icon.sprite = icon;
    }
}
