using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ChampAvatar Data",menuName ="GameData/ChampAvatar Data",order = 1)]
public class ChampionAvatarData : ScriptableObject
{
    /// <summary>
    /// This ScriptableObject defines a Player Character for BossRoom. It defines its CharacterClass field for
    /// associated game-specific properties, as well as its graphics representation.
    /// </summary>

    public ChampionData CharacterClass;

    public GameObject Graphics;

    public GameObject GraphicsCharacterSelect;

    public Sprite Portrait;
}


