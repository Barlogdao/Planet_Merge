using System;
using UnityEngine;

namespace PlanetMerge.Configs
{
    [Serializable]
    public struct PlanetSetup
    {
        public Vector2 Position;
        [Range(0, 6)] public int RankModifier;
    }
}