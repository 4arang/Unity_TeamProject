using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Serialization;

public class NetworkAvatarGuidState : MonoBehaviourPunCallbacks
{    
    [FormerlySerializedAs("AvatarGuidArray")]
    [HideInInspector]
    //public NetworkVariable<NetworkGuid> AvatarGuid = new NetworkVariable<NetworkGuid>();

    CharacterClassContainer m_CharacterClassContainer;

    [SerializeField]
    AvatarRegistry m_AvatarRegistry;

    AceofWings.Avatar m_Avatar;
    public AceofWings.Avatar RegisteredAvatar
    {
        get
        {
            if (m_Avatar == null)
            {
                //RegisterAvatar(AvatarGuid.Value.ToGuid());
            }

            return m_Avatar;
        }
    }

    private void Awake()
    {
        m_CharacterClassContainer = GetComponent<CharacterClassContainer>();
    }

    public void RegisterAvatar(Guid guid)
    {
        if (guid.Equals(Guid.Empty))
        {
            // not a valid Guid
            return;
        }

        // based on the Guid received, Avatar is fetched from AvatarRegistry
        if (!m_AvatarRegistry.TryGetAvatar(guid, out AceofWings.Avatar avatar))
        {
            Debug.LogError("Avatar not found!");
            return;
        }

        if (m_Avatar != null)
        {
            // already set, this is an idempotent call, we don't want to Instantiate twice
            return;
        }

        m_Avatar = avatar;

        //m_CharacterClassContainer.SetCharacterClass(avatar.CharacterClass);
    }
}
