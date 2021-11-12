using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerUI : MonoBehaviour
{
	#region SINGLETON
	private static PlayerUI instance;

	public static PlayerUI Instance
	{
		get
		{
			if (instance == null)
			{
				var obj = FindObjectOfType<PlayerUI>();
				if (obj != null)
				{
					instance = obj;
				}
				else
				{
					var newObj = new GameObject().AddComponent<PlayerUI>();        //배포 시, 활성화
					instance = newObj;
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		var objs = FindObjectsOfType<PlayerUI>();
		if (objs.Length != 1)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	#region SEATS
	int myChampIdx;
	float deltaTime = 0.0f; //Check FPS

	[SerializeField] private Text FPSText;
	[SerializeField] private Text MSText;
	[SerializeField] private Text TimeText;
	[SerializeField] private Text redKills;
	[SerializeField] private Text blueKills;
	[SerializeField] private Text playerScore;
	[SerializeField] private Text playerMinionScore;
	[SerializeField] private Text playerHealth;
	[SerializeField] private Text playerResource;
	[SerializeField] private Text playerGold;
	[SerializeField] private Image scoreBoardPanel;

	[SerializeField] private GameObject myChampObj;
	[SerializeField] private Image champPortrait;

	[SerializeField] private List<Image> selectedChampAbilities;
	[SerializeField] private List<Image> selectedChampAbilities_BW;
	[SerializeField] private List<Image> selectedSummonerSpells;

	[SerializeField]
	private Image recallButton;
	#region Private Fields
	/// <summary>
	/// Player UI. Constraint the UI to follow a PlayerManager GameObject in the world,
	/// Affect a slider and text to display Player's name and health
	/// </summary>
	[Header("Stats")]
	[SerializeField] private Text LevelText;
	[SerializeField] private Text ADText;
	[SerializeField] private Text APText;
	[SerializeField] private Text ArmorText;
	[SerializeField] private Text MagicResistText;
	[SerializeField] private Text AbilityHasteText;
	[SerializeField] private Text MoveSpeedText;
	[SerializeField] private Text CriticalStrikeText;
	[SerializeField] private Text AttackSpeedText;

	[SerializeField] private GameObject playerActionBox;
	[SerializeField] private GameObject playerStatusBox;

    #endregion

    AvatarManager localPlayer;

	float characterControllerHeight;

	Transform targetTransform;		//to mapping local player

	Renderer targetRenderer;

	CanvasGroup _canvasGroup;

    #endregion

    #region MonoBehaviour Messages

    private void Start()
    {
		_canvasGroup = this.GetComponent<CanvasGroup>();
		this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
		GameInit();

		myChampIdx = TestInfo.PI.mySelectedChampion;
	}
    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity on every frame.
    /// update the health slider to reflect the Player's health
    /// </summary>
    void Update()
	{
		UpdateGameStatus();


		// Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
		if (localPlayer == null)
		{
			Destroy(this.gameObject);
			return;
		}
	}
    #region Public Methods

    /// <summary>
    /// Assigns a Player Target to Follow and represent.
    /// </summary>
    /// <param name="target">Target.</param>

    public void SetTarget(AvatarManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        // Cache references for efficiency because we are going to reuse them.
        this.localPlayer = _target;
        targetTransform = this.localPlayer.GetComponent<Transform>();
    }

    public void SetPortrait()
    {
		champPortrait.sprite = GameDataSource.Instance.m_CharacterData[myChampIdx].Portrait;
		Debug.Log(GameDataSource.Instance.m_CharacterData[myChampIdx].Portrait.name);
    }
	public void SetActionBar(int whichChamp)
    {
        switch (whichChamp)
        {
            case 0: //Baekrang
                {
                    for (int i = 0; i < selectedChampAbilities.Count; i++)
                    {
						selectedChampAbilities[i].sprite = GameDataSource.Instance.m_BaekRangSkillData[i].Icon;
						selectedChampAbilities_BW[i].sprite = GameDataSource.Instance.m_BaekRangSkillData[i].Icon;

						selectedChampAbilities_BW[i].fillAmount = 1;
					}
                    break;
                }
            case 1: //ColD
                {
                    for (int i = 0; i < selectedChampAbilities.Count; i++)
                    {
						selectedChampAbilities[i].sprite = GameDataSource.Instance.m_ColDSkillData[i].Icon;
						selectedChampAbilities_BW[i].sprite = GameDataSource.Instance.m_ColDSkillData[i].Icon;

						selectedChampAbilities_BW[i].fillAmount = 1;
					}
					break;
                }
            case 2: //Xerion
                {
                    for (int i = 0; i < selectedChampAbilities.Count; i++)
                    {
						selectedChampAbilities[i].sprite = GameDataSource.Instance.m_XerionSkillData[i].Icon;
						selectedChampAbilities_BW[i].sprite = GameDataSource.Instance.m_XerionSkillData[i].Icon;

						selectedChampAbilities_BW[i].fillAmount = 1;
					}
					break;
                }

        }

    }

	public void SetSpells()
    {
		//selectedSummonerSpells[0].Icon = 
		//	GameDataSource.Instance.m_SpellData[PlayerInfo.PI.mySelectedSpell1].Icon;
		//selectedSummonerSpells[1].Icon =
		//	GameDataSource.Instance.m_SpellData[PlayerInfo.PI.mySelectedSpell2].Icon;
	}
	public void SetStatus()
    {
		Player_Stats stats=myChampObj.GetComponent<Player_Stats>();

		ADText.text = stats.AD.ToString();
		APText.text = stats.AP.ToString();
		ArmorText.text = stats.AP.ToString();
		MagicResistText.text = stats.MRP.ToString();
		AbilityHasteText.text = stats.AP.ToString();
		MoveSpeedText.text = stats.MoveSpeed.ToString();
		CriticalStrikeText.text = stats.AP.ToString();
		AttackSpeedText.text = stats.AttackSpeed.ToString();
		LevelText.text = stats.Level.ToString();
    }

	public void SetChampion()
    {
		GameObject champ = GameObject.Find("PlayerAvatar(Clone)");
		TestSetup myChampSetup = champ.GetComponent<TestSetup>();
		PhotonView PV = champ.GetPhotonView();
		Debug.Log("나의 아바타는 ="+champ.name);
		if(PV.IsMine)
        {
			myChampObj = myChampSetup.myCharacter;
			Debug.Log("오브젝트 이게 바인딩 됨" + myChampObj.name);
        }
			
    }
	public void GameInit()
	{
		SetChampion();					//UI, Champ Binding.
		SetPortrait();					//Champ Portrait Update
		SetActionBar(myChampIdx);		//Champ Actionbar Init
		SetStatus();					//Champ status Update
	}

	public void UpdateGameStatus()
    {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		MSText.text = (deltaTime * 1000.0f).ToString("0.0" + "ms");
		FPSText.text = (1.0f / deltaTime).ToString("0." + "fps");
		TimeText.text = GameManager.Instance.GameTime.ToString("00:00");
	}
    #endregion
    #endregion
}

