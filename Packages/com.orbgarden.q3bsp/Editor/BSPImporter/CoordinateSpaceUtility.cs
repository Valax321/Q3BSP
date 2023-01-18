using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal static class CoordinateSpaceUtility
    {
        public static Vector2 TransformQuakeToUnitySize(Vector2 quakeSize)
        {
            return quakeSize / 64;
        }
        
        public static Vector3 TransformQuakeToUnityDirection(Vector3 quakeDir)
        {
            return new Vector3(quakeDir.x, quakeDir.z, quakeDir.y);
        }

        public static Vector3 TransformQuakeToUnityPosition(Vector3 quakePos)
        {
            return new Vector3(quakePos.x / 64, quakePos.z / 64, quakePos.y / 64);
        }
    }
}