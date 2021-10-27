using UnityEngine;

public class CharacterClassContainer : MonoBehaviour
{
    [SerializeField]
    ChampionData m_CharacterClass;

    public ChampionData CharacterClass
    {
        get
        {
            if (m_CharacterClass == null)
            {
                //m_CharacterClass = m_State.RegisteredAvatar.CharacterClass;
            }

            return m_CharacterClass;
        }
    }

    //private NetworkAvatarGuidState m_State;

    //private void Awake()
    //{
    //    m_State = GetComponent<NetworkAvatarGuidState>();
    //}

    //public void SetCharacterClass(CharacterClass characterClass)
    //{
    //    m_CharacterClass = characterClass;
    //}
}
