using UnityEngine;

[CreateAssetMenu(menuName = "GameData/ChampionClass", order = 1)]
public class ChampionClass : ScriptableObject
{   
    [Tooltip("which character this data represents")]
    public GameConsts.CharacterTypeEnum CharacterType;

    [Tooltip("skill1 is usually the character's default attack")]
    public ActionType Skill1;

    [Tooltip("skill2 is usually the character's secondary attack")]
    public ActionType Skill2;

    [Tooltip("skill3 is usually the character's unique or special attack")]
    public ActionType Skill3;

    [Tooltip("skill3 is usually the character's unique or special attack")]
    public ActionType Skill4;

    [Tooltip("skill3 is usually the character's unique or special attack")]
    public ActionType Skill5;

    //[Tooltip("Starting HP of this character class")]
    //public IntVariable BaseHP;

    //[Tooltip("Starting Mana of this character class")]
    //public int BaseMana;

    //[Tooltip("Base movement speed of this character class (in meters/sec)")]
    //public float Speed;

    [Tooltip("Set to true if this represents an NPC, as opposed to a player.")]
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
}

