using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class BSPLeafFace
    {
        public const int LENGTH = 4;
        
        public int face { get; set; }

        public BSPLeafFace(BinaryReader reader)
        {
            face = reader.ReadInt32();
        }
    }
}