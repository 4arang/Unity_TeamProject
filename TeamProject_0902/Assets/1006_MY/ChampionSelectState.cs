using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Client specialization of the Character Select game state. Mainly controls the UI during character-select.
/// </summary>
[RequireComponent(typeof(ChampionSelectData))]
public class ChampionSelectState : GameStateBehaviour
{
    public override GameState ActiveState => throw new NotImplementedException();

    /// <summary>
    /// Reference to the scene's state object so that UI can access state
    /// </summary>
    public static ChampionSelectState Instance { get; private set; }

    //public override GameState ActiveState { get { return GameState.CharSelect; } }
    public ChampionSelectData CharSelectData { get; private set; }

    /// <summary>
    /// Internal utility that sets the character-graphics and class-info box based on
    /// our chosen seat. It also triggers a LobbyMode change when it notices that our seat-state
    /// is LockedIn.
    /// </summary>
    /// <param name="state">Our current seat state</param>
    /// <param name="seatIdx">Which seat we're sitting in, or -1 if SeatState is Inactive</param>
    //private void UpdateCharacterSelection(CharSelectData.SeatState state, int seatIdx = -1)
    //{
    //    bool isNewSeat = m_LastSeatSelected != seatIdx;

    //    m_LastSeatSelected = seatIdx;
    //    if (state == CharSelectData.SeatState.Inactive)
    //    {
    //        if (m_CurrentCharacterGraphics)
    //        {
    //            m_CurrentCharacterGraphics.SetActive(false);
    //        }

    //        m_ClassInfoBox.ConfigureForNoSelection();
    //    }
    //    else
    //    {
    //        if (seatIdx != -1)
    //        {
    //            // change character preview when selecting a new seat
    //            if (isNewSeat)
    //            {
    //                var selectedCharacterGraphics = GetCharacterGraphics(CharSelectData.AvatarConfiguration[seatIdx]);

    //                if (m_CurrentCharacterGraphics)
    //                {
    //                    m_CurrentCharacterGraphics.SetActive(false);
    //                }

    //                selectedCharacterGraphics.SetActive(true);
    //                m_CurrentCharacterGraphics = selectedCharacterGraphics;
    //                m_CurrentCharacterGraphicsAnimator = m_CurrentCharacterGraphics.GetComponent<Animator>();

    //                m_ClassInfoBox.ConfigureForClass(CharSelectData.AvatarConfiguration[seatIdx].CharacterClass);
    //            }
    //        }
    //        if (state == CharSelectData.SeatState.LockedIn && !m_HasLocalPlayerLockedIn)
    //        {
    //            // the local player has locked in their seat choice! Rearrange the UI appropriately
    //            // the character should act excited
    //            m_CurrentCharacterGraphicsAnimator.SetTrigger(m_AnimationTriggerOnCharChosen);
    //            ConfigureUIForLobbyMode(CharSelectData.IsLobbyClosed.Value ? LobbyMode.LobbyEnding : LobbyMode.SeatChosen);
    //            m_HasLocalPlayerLockedIn = true;
    //        }
    //        else if (m_HasLocalPlayerLockedIn && state == CharSelectData.SeatState.Active)
    //        {
    //            // reset character seats if locked in choice was unselected
    //            if (m_HasLocalPlayerLockedIn)
    //            {
    //                ConfigureUIForLobbyMode(LobbyMode.ChooseSeat);
    //                m_ClassInfoBox.SetLockedIn(false);
    //                m_HasLocalPlayerLockedIn = false;
    //            }
    //        }
    //        else if (state == CharSelectData.SeatState.Active && isNewSeat)
    //        {
    //            m_CurrentCharacterGraphicsAnimator.SetTrigger(m_AnimationTriggerOnCharSelect);
    //        }
    //    }
    //}
}
