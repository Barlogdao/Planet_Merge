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
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                ClickedDown?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ClickedUp?.Invoke();
            }
        }

        private bool IsPointerOverUI()
        {
            if (Agava.WebUtility.Device.IsMobile)
            {
                return EventSystem.current.IsPointerOverGameObject(0);
            }
            else
            {
                return EventSystem.current.IsPointerOverGameObject();
            }
        }
    }
}