using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class BSPLeafBrush
    {
        public const int LENGTH = 4;
        
        public int face { get; set; }

        public BSPLeafBrush(BinaryReader reader)
        {
            face = reader.ReadInt32();
        }
    }
}