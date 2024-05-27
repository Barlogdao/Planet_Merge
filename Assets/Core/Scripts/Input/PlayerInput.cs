using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlanetMerge.Systems
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] LayerMask _blockingLayer;

        public event Action ClickedDown;
        public event Action ClickedUp;

        public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
            {
                

                ClickedDown?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ClickedUp?.Invoke();
            }
        }

        private bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}