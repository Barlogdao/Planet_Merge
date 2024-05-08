using PlanetMerge.Planets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Systems
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action ClickedDown;
        public event Action ClickedUp;

        public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);  

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickedDown?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ClickedUp?.Invoke();
            }
        }
    }
}