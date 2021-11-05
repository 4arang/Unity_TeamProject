using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerUI : MonoBehaviour, IPunObservable
{
	private PhotonView PV;
	public AbilityData castingAbilitiy;
	int myChampIdx;

	[SerializeField]
	private GameObject myChampObj;

	[SerializeField]
	private Image champPortrait;

	[SerializeField]
	private List<AbilityData> selectedChampAbilities;

	[SerializeField]
	private List<AbilityData> selectedSummonerSpells;

	/// <summary>
	/// Player UI. Constraint the UI to follow a PlayerManager GameObject in the world,
	/// Affect a slider and text to display Player's name and health
	/// </summary>
	#region Private Fields
	[Header("Stats")]

	[SerializeField]
	private Text ADText;

	[SerializeField]
	private Text APText;

	[SerializeField]
	private Text ArmorText;

	[SerializeField]
	private Text MagicResistText;

	[SerializeField]
	private Text AbilityHasteText;

	[SerializeField]
	private Text MoveSpeedText;

	[SerializeField]
	private Text CriticalStrikeText;

	[SerializeField]
	private Text AttackSpeedText;


	[Tooltip("Pixel offset from the player target")]
	[SerializeField]
	private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

	[Tooltip("Champion Action bar")]
	[SerializeField]
	private GameObject playerActionBox;

	[Tooltip("UI display Player's Status")]
	[SerializeField]
	private GameObject playerStatusBox;

	[Tooltip("Abilities Seat")]
	[SerializeField]
	private List<Image> abilityButtonSeats;

	[Tooltip("Summoner's Spell Seat")]
	[SerializeField]
	private List<Image> spellButtonSeats;

	[Tooltip("Summoner's Spell Seat")]
	[SerializeField]
	private Image recallButton;

	PlayerManager localPlayer;

	float characterControllerHeight;

	Transform targetTransform;		//to mapping local player

	Renderer targetRenderer;

	CanvasGroup _canvasGroup;


	#endregion

	#region MonoBehaviour Messages

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during early initialization phase
	/// </summary>
	void Awake()
	{
		_canvasGroup = this.GetComponent<CanvasGroup>();

		this.transform.SetParent(GameObject.Find("UICanvas").GetComponent<Transform>(), false);
	}

    private void Start()
    {
		PV = GetComponent<PhotonView>();
		PV.RPC("RPC_GameInit", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
		if (PV.IsMine)
		{
			//PV.RPC("RPC_GameInit", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedChampion);
		}
		myChampIdx = PlayerInfo.PI.mySelectedChampion;
		champPortrait = GameObject.Find("Portrait Image").GetComponent<Image>();
	}
    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity on every frame.
    /// update the health slider to reflect the Player's health
    /// </summary>
    void Update()
	{
		// Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
		if (localPlayer == null)
		{
			Destroy(this.gameObject);
			return;
		}
	}

	/// <summary>
	/// MonoBehaviour method called after all Update functions have been called. This is useful to order script execution.
	/// In our case since we are following a moving GameObject, we need to proceed after the player was moved during a particular frame.
	/// </summary>
	void LateUpdate()
	{

		// Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI, but not the player itself.
		if (targetRenderer != null)
		{
			this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
		}
	}
	#endregion

	#region Public Methods

	/// <summary>
	/// Assigns a Player Target to Follow and represent.
	/// </summary>
	/// <param name="target">Target.</param>

	public void SetTarget(PlayerManager _target)
	{
		if (_target == null)
		{
			Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
			return;
		}

		// Cache references for efficiency because we are going to reuse them.
		this.localPlayer = _target;
		targetTransform = this.localPlayer.GetComponent<Transform>();
		targetRenderer = this.localPlayer.GetComponentInChildren<Renderer>();


		CharacterController _characterController = this.localPlayer.GetComponent<CharacterController>();

		// Get data from the Player that won't change during the lifetime of this Component
		if (_characterController != null)
		{
			characterControllerHeight = _characterController.height;
		}
	}
	public void SetPortrait()
    {
		champPortrait.sprite = GameDataSource.Instance.m_AvatarData[myChampIdx].Portrait;
		Debug.Log(GameDataSource.Instance.m_AvatarData[myChampIdx].Portrait.name);
    }
	public void SetActionBar(int whichChamp)
    {
		switch(whichChamp)
        {
			case 0: //Baekrang
                {
					for (int i = 0; i < abilityButtonSeats.Count; i++)
					{
						abilityButtonSeats[i].sprite = GameDataSource.Instance.m_BaekRangSkillData[i].Icon;
					}
					break;
                }
			case 1: //ColD
                {
					for (int i = 0; i < abilityButtonSeats.Count; i++)
					{
						abilityButtonSeats[i].sprite = GameDataSource.Instance.m_ColDSkillData[i].Icon;
					}
					break;
                }
			case 2: //Xerion
                {
					for (int i = 0; i < abilityButtonSeats.Count; i++)
					{
						abilityButtonSeats[i].sprite = GameDataSource.Instance.m_XerionSkillData[i].Icon;
					}
					break;
                }

        }
		
	}

	public void SetSpells()
    {
		selectedSummonerSpells[0].Icon = 
			GameDataSource.Instance.m_SpellData[PlayerInfo.PI.mySelectedSpell1].Icon;
		selectedSummonerSpells[1].Icon =
			GameDataSource.Instance.m_SpellData[PlayerInfo.PI.mySelectedSpell2].Icon;
	}
	public void SetStatus()
    {
		//ADText=;


		//APText;


		//ArmorText;

		//MagicResistText;

		//AbilityHasteText;

		//MoveSpeedText;

		//CriticalStrikeText;

		//AttackSpeedText;
	}

	public void SetChampion()
    {
		GameObject champ = GameObject.Find("PlayerAvatar(Clone)");
		ChampionSetup myChampSetup = champ.GetComponent<ChampionSetup>();
		PhotonView PV = champ.GetPhotonView();
		Debug.Log("나의 아바타는 ="+champ.name);
		if(PV.IsMine)
        {
			myChampObj = myChampSetup.myCharacter;
			Debug.Log("오브젝트 이게 바인딩 됨" + myChampObj.name);
        }
			
    }
	#endregion


	#region PHOTON CALLBACKS
	[PunRPC]
	public void RPC_GameInit()
	{
		SetChampion();					//UI, Champ Binding.
		SetPortrait();					//Champ Portrait Update
		SetActionBar(myChampIdx);		//Champ Actionbar Init
		SetStatus();					//Champ status Update
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
    #endregion

}
