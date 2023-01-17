using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPPlane
    {
        public const int LENGTH = 4 * 3 + 4; // 3x float + 1 more float
        
        public Vector3 normal { get; set; }
        private float dist { get; set; }

        public BSPPlane(BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();

            normal = CoordinateSpaceUtility.TransformQuakeToUnityDirection(new Vector3(x, y, z));
            dist = reader.ReadSingle();
        }
    }
}