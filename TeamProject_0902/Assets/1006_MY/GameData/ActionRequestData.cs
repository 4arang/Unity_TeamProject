using System;
using UnityEngine;

public enum ActionType
{
    None,
    BaekRangBaseAttack,
    ColDBaseAttack,
    XerionBaseAttack,
    BaekRangAbility1,
    BaekRangAbility2,
    BaekRangAbility3,
    BaekRangAbility4,
    BaekRangAbility5,
    ColDAbility1,
    ColDAbility2,
    ColDAbility3,
    ColDAbility4,
    ColDAbility5,
    XerionAbility1,
    XerionAbility2,
    XerionAbility3,
    XerionAbility4,
    XerionAbility5,
    SummonerSpell
}


/// <summary>
/// List of all Types of Actions. There is a many-to-one mapping of Actions to ActionLogics.
/// </summary>
public enum ActionLogic
{
    Melee,
    RangedTargeted,
    Chase,
    Revive,
    LaunchProjectile,
    Emote,
    RangedFXTargeted,
    AoE,
    Trample,
    ChargedShield,
    Stunned,
    Target,
    ChargedLaunchProjectile,
    StealthMode,
    DashAttack,
    //O__O adding a new ActionLogic branch? Update Action.MakeAction!
}


/// <summary>
/// Comprehensive class that contains information needed to play back any action on the server. This is what gets sent client->server when
/// the Action gets played, and also what gets sent server->client to broadcast the action event. Note that the OUTCOMES of the action effect
/// don't ride along with this object when it is broadcast to clients; that information is sync'd separately, usually by NetworkVariables.
/// </summary>
public struct ActionRequestData
{
    public ActionType ActionTypeEnum;      //the action to play.
    public Vector3 Position;           //center position of skill, e.g. "ground zero" of a fireball skill.
    public Vector3 Direction;          //direction of skill, if not inferrable from the character's current facing.
    public ulong[] TargetIds;          //NetworkObjectIds of targets, or null if untargeted.
    public float Amount;               //can mean different things depending on the Action. For a ChaseAction, it will be target range the ChaseAction is trying to achieve.
    public bool ShouldQueue;           //if true, this action should queue. If false, it should clear all current actions and play immediately.
    public bool ShouldClose;           //if true, the server should synthesize a ChaseAction to close to within range of the target before playing the Action. Ignored for untargeted actions.
    public bool CancelMovement;        // if true, movement is cancelled before playing this action

    /// <summary>
    /// Returns true if the ActionRequestDatas are "functionally equivalent" (not including their Queueing or Closing properties).
    /// </summary>
    public bool Compare(ref ActionRequestData rhs)
    {
        bool scalarParamsEqual = (ActionTypeEnum, Position, Direction, Amount) == (rhs.ActionTypeEnum, rhs.Position, rhs.Direction, rhs.Amount);
        if (!scalarParamsEqual) { return false; }

        if (TargetIds == rhs.TargetIds) { return true; } //covers case of both being null.
        if (TargetIds == null || rhs.TargetIds == null || TargetIds.Length != rhs.TargetIds.Length) { return false; }
        for (int i = 0; i < TargetIds.Length; i++)
        {
            if (TargetIds[i] != rhs.TargetIds[i]) { return false; }
        }

        return true;
    }
}