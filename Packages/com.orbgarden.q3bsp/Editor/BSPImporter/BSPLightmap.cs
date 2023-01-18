using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    public class BSPLightmap
    {
        public const int LENGTH = 128 * 128 * 3;
        
        public Color[] map { get; set; } = new Color[128 * 128];

        public BSPLightmap(BinaryReader reader)
        {
            // This is really inefficient
            
            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    map[y * 128 + x].r = reader.ReadByte() / 256.0f;
                    map[y * 128 + x].g = reader.ReadByte() / 256.0f;
                    map[y * 128 + x].b = reader.ReadByte() / 256.0f;
                    map[y * 128 + x].a = 1.0f;
                }
            }
        }
    }
}