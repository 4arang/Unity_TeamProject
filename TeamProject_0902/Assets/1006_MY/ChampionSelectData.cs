using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
/// <summary>
/// Common data and RPCs for the CharSelect stage.
/// </summary>
public class ChampionSelectData : MonoBehaviourPunCallbacks
{
    public enum SeatState : byte
    {
        Inactive,
        Active,
        LockedIn,
    }
    public struct LobbyPlayerState
    {
        public ulong ClientId;

        private Player m_PlayerName; // I'm sad there's no 256Bytes fixed list :(

        public int PlayerNum; // this player's assigned "P#". (0=P1, 1=P2, etc.)
        public int SeatIdx; // the latest seat they were in. -1 means none
        public float LastChangeTime;

        public SeatState SeatState;


        public LobbyPlayerState(ulong clientId, string name, int playerNum, SeatState state, int seatIdx = -1, float lastChangeTime = 0)
        {
            ClientId = clientId;
            PlayerNum = playerNum;
            SeatState = state;
            SeatIdx = seatIdx;
            LastChangeTime = lastChangeTime;
            m_PlayerName = new Player();

            PlayerName = name;
        }

        public string PlayerName
        {
            get => m_PlayerName.ToString();
            private set => m_PlayerName.ToString();
        }

        //public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        //{
        //    serializer.SerializeValue(ref ClientId);
        //    serializer.SerializeValue(ref m_PlayerName);
        //    serializer.SerializeValue(ref PlayerNum);
        //    serializer.SerializeValue(ref SeatState);
        //    serializer.SerializeValue(ref SeatIdx);
        //    serializer.SerializeValue(ref LastChangeTime);
        //}

        public bool Equals(LobbyPlayerState other)
        {
            return ClientId == other.ClientId &&
                   m_PlayerName.Equals(other.m_PlayerName) &&
                   PlayerNum == other.PlayerNum &&
                   SeatIdx == other.SeatIdx &&
                   LastChangeTime.Equals(other.LastChangeTime) &&
                   SeatState == other.SeatState;
        }
    }

    //private NetworkList<LobbyPlayerState> m_LobbyPlayers;
    
    //public AceofWings.Avatar[] AvatarConfiguration;

    //private void Awake()
    //{
    //    m_LobbyPlayers = new NetworkList<LobbyPlayerState>();
    //}

    ///// <summary>
    ///// Current state of all players in the lobby.
    ///// </summary>
    //public NetworkList<LobbyPlayerState> LobbyPlayers => m_LobbyPlayers;

    ///// <summary>
    ///// When this becomes true, the lobby is closed and in process of terminating (switching to gameplay).
    ///// </summary>
    //public NetworkVariable<bool> IsLobbyClosed { get; } = new NetworkVariable<bool>(false);

    ///// <summary>
    ///// Server notification when a client requests a different lobby-seat, or locks in their seat choice
    ///// </summary>
    //public event Action<ulong, int, bool> OnClientChangedSeat;

    ///// <summary>
    ///// RPC to notify the server that a client has chosen a seat.
    ///// </summary>
    //[ServerRpc(RequireOwnership = false)]
    //public void ChangeSeatServerRpc(ulong clientId, int seatIdx, bool lockedIn)
    //{
    //    OnClientChangedSeat?.Invoke(clientId, seatIdx, lockedIn);
    //}
}

