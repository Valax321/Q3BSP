using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPVertex
    {
        public const int LENGTH = 10 * 4 + 4;
        
        public Vector3 position { get; set; }
        public Vector2 texCoord0 { get; set; }
        public Vector2 texCoord1 { get; set; }
        public Vector3 normal { get; set; }
        public Color color { get; set; }

        public BSPVertex(BinaryReader reader)
        {
            position = reader.ReadVector3Pos();
            texCoord0 = reader.ReadVector2();
            texCoord1 = reader.ReadVector2();
            normal = reader.ReadVector3Dir();
            color = reader.ReadColorRGBA();
        }
    }
}