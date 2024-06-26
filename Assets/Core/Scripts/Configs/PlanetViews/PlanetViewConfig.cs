﻿using NaughtyAttributes;
using UnityEngine;

namespace PlanetMerge.Configs.PlanetViews
{
    [CreateAssetMenu(fileName = "PlanetViewConfig", menuName = "Configs/PlanetViewConfig", order = 1)]
    public class PlanetViewConfig : ScriptableObject
    {
        [SerializeField, ShowAssetPreview] private Sprite _sprite;

        [field: SerializeField] public Color LabelColor { get; private set; } = Color.white;

        public Sprite Sprite => _sprite;
    }
}