﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PlanetMerge.Utils.Tweens
{
    [System.Serializable]
    public class AppearTween
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Ease _ease;
        [SerializeField] private Vector2 _appearPosition;

        public async UniTask RunAsync(Transform transform)
        {
            await transform.DOMove(_appearPosition, _duration).From().SetEase(_ease);
        }
    }
}