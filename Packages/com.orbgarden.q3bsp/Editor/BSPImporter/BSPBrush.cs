using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
{
    public class BSPBrush
    {
        public const int LENGTH = 3 * 4;
        
        public int brushSide { get; set; }
        public int numBrushSides { get; set; }
        public int texture { get; set; }

        public BSPBrush(BinaryReader reader)
        {
            brushSide = reader.ReadInt32();
            numBrushSides = reader.ReadInt32();
            texture = reader.ReadInt32();
        }
    }
}