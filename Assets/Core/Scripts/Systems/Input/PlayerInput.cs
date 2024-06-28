using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlanetMerge.Systems
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action ClickedDown;

        public event Action ClickedUp;

        public Vector2 PointerPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private void Update()
        {
            if (IsGameClickDownDetected())
            {
                ClickedDown?.Invoke();
                return;
            }

            if (IsGameClickUpDetected())
            {
                ClickedUp?.Invoke();
                return;
            }
        }

        private bool IsGameClickDownDetected()
        {
            return Input.GetMouseButtonDown(0) && IsPointerOverUI() == false;
        }

        private bool IsGameClickUpDetected()
        {
            return Input.GetMouseButtonUp(0);
        }

        private bool IsPointerOverUI()
        {
            if (Agava.WebUtility.Device.IsMobile)
                return EventSystem.current.IsPointerOverGameObject(0);

            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}