﻿using UnityEngine;

namespace PlanetMerge.Configs.Layouts
{
    [System.Serializable]
    public struct PlanetSetup
    {
        [Range(-1.9f, 1.9f)] public float PositionX;
        [Range(-2.3f, 2.3f)] public float PositionY;
        [Range(0, 6)] public int RankModifier;

        public Vector2 Position => new Vector2(PositionX, PositionY);
    }
}