using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public enum LifeState
{
    Alive,
    Fainted,
    Dead,
}

/// <summary>
/// Describes how the character's movement should be animated: as standing idle, running normally,
/// magically slowed, sped up, etc. (Not all statuses are currently used by game content,
/// but they are set up to be displayed correctly for future use.)
/// </summary>
[Serializable]
public enum MovementStatus
{
    Idle,         // not trying to move
    Normal,       // character is moving (normally)
    Uncontrolled, // character is being moved by e.g. a knockback -- they are not in control!
    Slowed,       // character's movement is magically hindered
    Hasted,       // character's movement is magically enhanced
    Walking,      // character should appear to be "walking" rather than normal running (e.g. for cut-scenes)

    //CC skill state will update
}
public class NetworkChampionState : MonoBehaviourPunCallbacks
{
    [SerializeField]
    CharacterClassContainer m_CharacterClassContainer;

    /// <summary>
    /// The CharacterData object associated with this Character. This is the static game data that defines its attack skills, HP, etc.
    /// </summary>
    public ChampionData CharacterClass => m_CharacterClassContainer.CharacterClass;

    /// <summary>
    /// Character Type. This value is populated during character selection.
    /// </summary>
    public CharacterTypeEnum CharacterType => m_CharacterClassContainer.CharacterClass.CharacterType;

    /// <summary>
    /// Gets invoked when inputs are received from the client which own this networked character.
    /// </summary>
    public event Action<Vector3> ReceivedClientInput;

    #region ACTION SYSTEM

    /// <summary>
    /// This event is raised on the server when an action request arrives
    /// </summary>
    public event Action<ActionRequestData> DoActionEventServer;

    /// <summary>
    /// This event is raised on the client when an action is being played back.
    /// </summary>
    public event Action<ActionRequestData> DoActionEventClient;

    /// <summary>
    /// This event is raised on the client when the active action FXs need to be cancelled (e.g. when the character has been stunned)
    /// </summary>
    public event Action CancelAllActionsEventClient;

    /// <summary>
    /// This event is raised on the client when active action FXs of a certain type need to be cancelled (e.g. when the Stealth action ends)
    /// </summary>
    public event Action<ActionType> CancelActionsByTypeEventClient;

    /// <summary>
    /// /// Server to Client RPC that broadcasts this action play to all clients.
    /// </summary>
    /// <param name="data"> Data about which action to play and its associated details. </param>
    [PunRPC]
    public void RecvDoActionClientRPC(ActionRequestData data)
    {
        DoActionEventClient?.Invoke(data);
    }

    [PunRPC]
    public void RecvCancelAllActionsClientRpc()
    {
        CancelAllActionsEventClient?.Invoke(); 
    }

    [PunRPC]
    public void RecvCancelActionsByTypeClientRpc(ActionType action)
    {
        CancelActionsByTypeEventClient?.Invoke(action);
    }

    /// <summary>
    /// Client->Server RPC that sends a request to play an action.
    /// </summary>
    /// <param name="data">Data about which action to play and its associated details. </param>
    [PunRPC]
    public void RecvDoActionServerRPC(ActionRequestData data)
    {
        DoActionEventServer?.Invoke(data);
    }

    // UTILITY AND SPECIAL-PURPOSE RPCs

    /// <summary>
    /// Called on server when the character's client decides they have stopped "charging up" an attack.
    /// </summary>
    public event Action OnStopChargingUpServer;

    /// <summary>
    /// Called on all clients when this character has stopped "charging up" an attack.
    /// Provides a value between 0 and 1 inclusive which indicates how "charged up" the attack ended up being.
    /// </summary>
    public event Action<float> OnStopChargingUpClient;

    [PunRPC]
    public void RecvStopChargingUpServerRpc()
    {
        OnStopChargingUpServer?.Invoke();
    }

    [PunRPC]
    public void RecvStopChargingUpClientRpc(float percentCharged)
    {
        OnStopChargingUpClient?.Invoke(percentCharged);
    }
    #endregion
}
    

