using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerUI : MonoBehaviour
{
	public AbilityData actionAbilities;
	int myChampIdx;
	[SerializeField]
	private Image champPortrait;

	/// <summary>
	/// Player UI. Constraint the UI to follow a PlayerManager GameObject in the world,
	/// Affect a slider and text to display Player's name and health
	/// </summary>
	#region Private Fields

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
		myChampIdx = PlayerInfo.PI.mySelectedChampion;
		champPortrait = GameObject.Find("Portrait Image").GetComponent<Image>();
		Debug.Log(champPortrait.name);
		GameInit();
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
	
	public void GameInit()
    {
		SetPortrait();
    }
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
	public void SetActionBar(PlayerManager whichChamp)
    {

    }

	public void SetStatus(PlayerManager whichChamp)
    {

    }
	#endregion


}
