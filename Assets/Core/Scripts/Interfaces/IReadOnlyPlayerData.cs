using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReadOnlyPlayerData
{
    int Level { get; }
    int PlanetRank { get; }
    int Score { get; }
}
