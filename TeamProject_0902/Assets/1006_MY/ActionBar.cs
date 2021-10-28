using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The button that activates the hero's first special move")]
    UIHUDButton m_SpecialAction1Button;

    [SerializeField]
    [Tooltip("The button that activates the hero's second special move")]
    UIHUDButton m_SpecialAction2Button;

    [SerializeField]
    [Tooltip("The button that activates the hero's first special move")]
    UIHUDButton m_SpecialAction3Button;

    [SerializeField]
    [Tooltip("The button that activates the hero's second special move")]
    UIHUDButton m_SpecialAction4Button;

    [SerializeField]
    [Tooltip("The button that activates the hero's first special move")]
    UIHUDButton m_SpecialAction5Button;

    [SerializeField]
    [Tooltip("The button that activates the hero's first special move")]
    UIHUDButton m_SummonerSpell1Button;

    [SerializeField]
    [Tooltip("The button that activates the hero's first special move")]
    UIHUDButton m_SummonerSpell2Button;

    [SerializeField]
    [Tooltip("The button that opens/closes the Emote bar")]
    UIHUDButton m_EmoteBarButton;

    /// <summary>
    /// Identifiers for the buttons on the action bar.
    /// </summary>
    enum ActionButtonType
    {
        Ability1,
        Ability2,
        Ability3,
        Ability4,
        Ability5,
        SummonerSpell1,
        SummonerSpell2,
        EmoteBar
    }

    /// <summary>
    /// Cached reference to local player's net state.
    /// We find the Sprites to use by checking the Skill1, Skill2, and Skill3 members of our chosen CharacterClass
    /// </summary>
    NetworkChampionState m_NetState;

    /// <summary>
    /// If we have another player selected, this is a reference to their stats; if anything else is selected, this is null
    /// </summary>
    NetworkChampionState m_SelectedPlayerNetState;
    class ActionButtonInfo
    {
        public readonly ActionButtonType Type;
        public readonly UIHUDButton Button;
        public readonly UITooltipDetector Tooltip;

        /// <summary>
        /// The current ActionType that is used when this button is pressed.
        /// </summary>
        public ActionType CurActionType;

        readonly ActionBar m_Owner;

        public ActionButtonInfo(ActionButtonType type, UIHUDButton button, ActionBar owner)
        {
            Type = type;
            Button = button;
            Tooltip = button.GetComponent<UITooltipDetector>();
            CurActionType = ActionType.None;
            m_Owner = owner;
        }

        public void RegisterEventHandlers()
        {
            Button.OnPointerDownEvent += OnClickDown;
            Button.OnPointerUpEvent += OnClickUp;
        }

        public void UnregisterEventHandlers()
        {
            Button.OnPointerDownEvent -= OnClickDown;
            Button.OnPointerUpEvent -= OnClickUp;
        }

        void OnClickDown()
        {
            m_Owner.OnButtonClickedDown(Type);
        }

        void OnClickUp()
        {
            m_Owner.OnButtonClickedUp(Type);
        }
    }


    #region UI BUTTON CALLBACKS
    void OnButtonClickedDown(ActionButtonType buttonType)
    {
        if (buttonType == ActionButtonType.EmoteBar)
        {
            return; // this is the "emote" button; we won't do anyhing until they let go of the button
        }

        //if (m_InputSender == null)
        //{
        //    //nothing to do past this point if we don't have an InputSender.
        //    return;
        //}

        //// send input to begin the action associated with this button
        //m_InputSender.RequestAction(m_ButtonInfo[buttonType].CurActionType, SkillTriggerStyle.UI);
    }

    void OnButtonClickedUp(ActionButtonType buttonType)
    {
        //if (buttonType == ActionButtonType.EmoteBar)
        //{
        //    m_EmotePanel.SetActive(!m_EmotePanel.activeSelf);
        //    return;
        //}

        //if (m_InputSender == null)
        //{
        //    //nothing to do past this point if we don't have an InputSender.
        //    return;
        //}

        //// send input to complete the action associated with this button
        //m_InputSender.RequestAction(m_ButtonInfo[buttonType].CurActionType, SkillTriggerStyle.UIRelease);
    }
    #endregion


    /// <summary>
    /// Dictionary of info about all the buttons on the action bar.
    /// </summary>
    Dictionary<ActionButtonType, ActionButtonInfo> m_ButtonInfo;

    /// <summary>
    /// Cache the input sender from a <see cref="ClientPlayerAvatar"/> and self-initialize.
    /// </summary>
    /// <param name="clientPlayerAvatar"></param>                                                                                                                                                                                                                     
    //void RegisterInputSender(ClientPlayerAvatar clientPlayerAvatar)
    //{
    //    if (!clientPlayerAvatar.TryGetComponent(out ClientInputSender inputSender))
    //    {
    //        Debug.LogError("ClientInputSender not found on ClientPlayerAvatar!", clientPlayerAvatar);
    //    }

    //    if (m_InputSender != null)
    //    {
    //        Debug.LogWarning($"Multiple ClientInputSenders in scene? Discarding sender belonging to {m_InputSender.gameObject.name} and adding it for {inputSender.gameObject.name} ");
    //    }

    //    m_InputSender = inputSender;
    //    m_NetState = m_InputSender.GetComponent<NetworkCharacterState>();
    //    m_NetState.TargetId.OnValueChanged += OnSelectionChanged;
    //    UpdateAllActionButtons();
    //}

    void Awake()
    {
        m_ButtonInfo = new Dictionary<ActionButtonType, ActionButtonInfo>()
        {
            [ActionButtonType.Ability1] = new ActionButtonInfo(ActionButtonType.Ability1, m_SpecialAction1Button, this),
            [ActionButtonType.Ability2] = new ActionButtonInfo(ActionButtonType.Ability2, m_SpecialAction2Button, this),
            [ActionButtonType.Ability3] = new ActionButtonInfo(ActionButtonType.Ability3, m_SpecialAction3Button, this),
            [ActionButtonType.Ability4] = new ActionButtonInfo(ActionButtonType.Ability4, m_SpecialAction4Button, this),
            [ActionButtonType.Ability5] = new ActionButtonInfo(ActionButtonType.Ability5, m_SpecialAction5Button, this),
            [ActionButtonType.SummonerSpell1] = new ActionButtonInfo(ActionButtonType.SummonerSpell1, m_SummonerSpell1Button, this),
            [ActionButtonType.SummonerSpell2] = new ActionButtonInfo(ActionButtonType.SummonerSpell2, m_SummonerSpell2Button, this),
            [ActionButtonType.EmoteBar] = new ActionButtonInfo(ActionButtonType.EmoteBar, m_EmoteBarButton, this),
        };
    }
    void OnEnable()
    {
        foreach (ActionButtonInfo buttonInfo in m_ButtonInfo.Values)
        {
            buttonInfo.RegisterEventHandlers();
        }
    }

    void OnDisable()
    {
        foreach (ActionButtonInfo buttonInfo in m_ButtonInfo.Values)
        {
            buttonInfo.UnregisterEventHandlers();
        }
    }
    void OnDestroy()
    {
        //ClientPlayerAvatar.LocalClientSpawned -= RegisterInputSender;
        //ClientPlayerAvatar.LocalClientDespawned -= DeregisterInputSender;

        //if (m_NetState)
        //{
        //    m_NetState.TargetId.OnValueChanged -= OnSelectionChanged;
        //}
    }
   
}



