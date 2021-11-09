using System.Collections.Generic;
using UnityEngine;

public class GameDataSource : MonoBehaviour
{
    [Tooltip("All CharacterClass data should be slotted in here")]
    public List<ChampionData> m_CharacterData;

    [Tooltip("All ActionDescription data should be slotted in here")]
    public List<AbilityData> m_BaekRangSkillData;

    [Tooltip("All ActionDescription data should be slotted in here")]
    public List<AbilityData> m_ColDSkillData;

    [Tooltip("All ActionDescription data should be slotted in here")]
    public List<AbilityData> m_XerionSkillData;

    [Tooltip("All SummonerSpellDescription data should be slotted in here")]
    public List<AbilityData> m_SpellData;

    [Tooltip("All ActionDescription data should be slotted in here")]
    public List<AbilityData> m_ItemData;

    private Dictionary<CharacterTypeEnum, ChampionData> m_CharacterDataMap;
    private Dictionary<ActionType, AbilityData> m_ActionDataMap;

    /// <summary>
    /// static accessor for all GameData.
    /// </summary>
    public static GameDataSource Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            throw new System.Exception("Multiple GameDataSources defined!");
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
}

