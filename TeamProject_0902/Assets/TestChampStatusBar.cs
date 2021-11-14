using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
public class TestChampStatusBar : MonoBehaviourPunCallbacks
{
    [Tooltip("Pixel offset from the player target")]
    [SerializeField] private Vector3 fromChampOffset = new Vector3(0f, 3.0f, 0f);

    [Tooltip("UI Text to display Player's Name")]
    [SerializeField] private Text playerNameText;

    [Tooltip("UI Text to display Player's Level")]
    [SerializeField] private Text playerLevelText;

    [Tooltip("UI Text to display Player's Health")]
    [SerializeField] private Slider playerHealthSlider;

    [Tooltip("UI Text to display Player's Resource")]
    [SerializeField] private Slider playerResourceSlider;

    [Tooltip("Need to bind Gameobject when Start Game")]
    private GameObject myChamObj;

    Player_Stats stats;
    private void Start()
    {
        myChamObj = transform.parent.gameObject;
        stats = myChamObj.GetComponent<Player_Stats>();

        playerNameText.text = PhotonNetwork.LocalPlayer.NickName;       //Photon View Id로 닉네임 우선 할당

        playerLevelText.text = stats.Level.ToString();
    }

    private void Update()
    {
        playerHealthSlider.value = stats.CurrentHP/stats.MaxHP;
        playerResourceSlider.value = stats.CurrentMP/ stats.MaxMP;
    }

    private void LateUpdate()
    {
        //카메라에 각도 고정
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
