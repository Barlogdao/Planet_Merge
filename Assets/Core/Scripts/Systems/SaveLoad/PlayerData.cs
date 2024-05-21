using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetMerge.Data
{
    [System.Serializable]
    public class PlayerData:IReadOnlyPlayerData
    {
        [field:SerializeField] public int Level { get; set; }
        [field: SerializeField] public int PlanetRank { get; set; }
        [field: SerializeField] public int Score { get; set; }
    }
}