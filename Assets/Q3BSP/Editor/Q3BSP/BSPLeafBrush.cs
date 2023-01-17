using System.IO;

namespace OrbGarden.TrenchbroomImport.Q3BSP
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