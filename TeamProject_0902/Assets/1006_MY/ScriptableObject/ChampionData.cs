using UnityEngine;

[CreateAssetMenu(menuName = "GameData/CharacterClass", order = 1)]

public class ChampionData : ScriptableObject
{
    [Tooltip("which character this data represents")]
    public CharacterTypeEnum CharacterType;

    [Tooltip("skill1 is usually the character's passive")]
    public ActionType Skill1;

    [Tooltip("skill2 is usually the character's nique or special attack")]
    public ActionType Skill2;

    [Tooltip("skill3 is usually the character's unique or special attack")]
    public ActionType Skill3;

    [Tooltip("skill3 is usually the character's unique or special attack")]
    public ActionType Skill4;

    [Tooltip("skill3 is usually the character's unique or ultimate attack")]
    public ActionType Skill5;

    [Tooltip("Set to true if this represents an Champion, as opposed to a player.")]
    public bool IsChampion;

    [Tooltip("Set to true if this represents an NPC, as opposed to a player.")]
    public bool IsNpc;

    [Tooltip("For NPCs, this will be used as the aggro radius at which enemies wake up and attack the player")]
    public float DetectRange;

    [Tooltip("For players, this is the displayed \"class name\". (Not used for monsters)")]
    public string DisplayedName;

    [Tooltip("For players, this is the class banner (when active). (Not used for monsters)")]
    public Sprite ClassBannerLit;

    [Tooltip("For players, this is the class banner (when inactive). (Not used for monsters)")]
    public Sprite ClassBannerUnlit;

    public GameObject InGameAvatar;

    public GameObject LobbyAvatar;

    public Sprite Portrait;

    [SerializeField] private AbilityData[] abilityData;
}
