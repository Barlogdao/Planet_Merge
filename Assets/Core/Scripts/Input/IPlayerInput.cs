using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{

    event Action ClickedDown;
    event Action ClickedUp;

    Vector2 MousePosition { get; }

}
