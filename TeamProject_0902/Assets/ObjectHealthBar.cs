using UnityEngine;
using UnityEngine.UI;

public class ObjectHealthBar : MonoBehaviour
{
    [Tooltip("Pixel offset from the player target")]
    [SerializeField] private Vector3 fromChampOffset = new Vector3(0f, 3.0f, 0f);

    [Tooltip("UI Text to display Player's Health")]
    [SerializeField] private Slider objHealthSlider;

    [Tooltip("Need to bind Gameobject when Start Game")]
    private GameObject bindObject;

    Minion_Stats stats;

    void Start()
    {
        bindObject = transform.parent.gameObject;
        stats = bindObject.GetComponent<Minion_Stats>();
    }

    void Update()
    {
        objHealthSlider.value = stats.HP / stats.MaxHP;
    }

    private void LateUpdate()
    {
        //카메라에 각도 고정
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
