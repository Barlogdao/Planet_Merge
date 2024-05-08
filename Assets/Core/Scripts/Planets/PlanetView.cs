using DG.Tweening;
using PlanetMerge.Configs;
using PlanetMerge.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlanetMerge.Planets
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlanetView : MonoBehaviour
    {
        [SerializeField] private PlanetViewProvider _viewProvider;
        [SerializeField] private TextMeshProUGUI _rankLabel;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrenght;

        private Planet _planet;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _originScale;


        public void Initialize(Planet planet)
        {
            _planet = planet;
            _originScale = transform.localScale;
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _planet.Collided += OnCollide;
            _planet.Merged += OnMerge;
        }

        private void OnMerge(int rank)
        {
            Set(rank);
        }

        public void Set(int rank)
        {
            PlanetViewData data = _viewProvider.GetViewData(rank);


            _rankLabel.text = data.RankText;
            _rankLabel.color = data.LabelColor;
            _spriteRenderer.sprite = data.Sprite;
        }

        private void OnCollide()
        {
            transform.DOShakeScale(_shakeDuration, _shakeStrenght).OnComplete(()=> transform.localScale = _originScale);
        }

        private void OnDestroy()
        {
            _planet.Collided -= OnCollide;
            _planet.Merged -= OnMerge;
        }
    }
}