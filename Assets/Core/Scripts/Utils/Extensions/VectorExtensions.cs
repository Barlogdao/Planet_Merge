using UnityEngine;

namespace PlanetMerge.Utils.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
    }
}