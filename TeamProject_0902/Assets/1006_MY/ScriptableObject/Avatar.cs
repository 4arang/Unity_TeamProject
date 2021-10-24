using System;
using UnityEngine;

namespace AceOfWings
{
    /// <summary>
    /// This ScriptableObject defines a Player Character for AceOfWing. It defines its CharacterClass field for
    /// associated game-specific properties, as well as its graphics representation.
    /// </summary>
    [CreateAssetMenu]
    [Serializable]
    public class Avatar : GuidScriptableObject
    {
        public ChampionClass CharacterClass;

        public GameObject Graphics;

        public GameObject GraphicsCharacterSelect;

        public Sprite Portrait;
    }
}
