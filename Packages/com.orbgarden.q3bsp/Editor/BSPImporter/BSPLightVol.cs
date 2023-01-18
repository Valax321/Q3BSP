using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    public class BSPLightVol
    {
        public const int LENGTH = 3 + 3 + 2;
        
        public Color ambient { get; set; }
        public Color directional { get; set; }
        public Vector2 dir { get; set; }

        public BSPLightVol(BinaryReader reader)
        {
            ambient = reader.ReadColorRGB();
            directional = reader.ReadColorRGB();
            dir = reader.ReadVector2();
        }
    }
}