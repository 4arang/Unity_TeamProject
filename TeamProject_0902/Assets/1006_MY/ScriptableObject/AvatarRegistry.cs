using System;
using UnityEngine;

/// <summary>
/// This ScriptableObject will be the container for all possible Avatars inside BossRoom.
/// <see cref="AceofWings.Avatar"/>
/// </summary>
[CreateAssetMenu]
public class AvatarRegistry : ScriptableObject
{
    [SerializeField]
    AceofWings.Avatar[] m_Avatars;

    public bool TryGetAvatar(Guid guid, out AceofWings.Avatar avatarValue)
    {
        avatarValue = Array.Find(m_Avatars, avatar => avatar.Guid == guid);

        return avatarValue != null;
    }

    public AceofWings.Avatar GetRandomAvatar()
    {
        if (m_Avatars == null || m_Avatars.Length == 0)
        {
            return null;
        }

        return m_Avatars[UnityEngine.Random.Range(0, m_Avatars.Length)];
    }
}
