using System.IO;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    public class BSPBrushSide
    {
        public const int LENGTH = 2 * 4;
        
        public int plane { get; set; }
        public int texture { get; set; }

        public BSPBrushSide(BinaryReader reader)
        {
            plane = reader.ReadInt32();
            texture = reader.ReadInt32();
        }
    }
}