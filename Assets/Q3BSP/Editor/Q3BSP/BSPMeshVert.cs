using System.IO;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPMeshVert
    {
        public const int LENGTH = 4;
        
        public int offset { get; set; }

        public BSPMeshVert(BinaryReader reader)
        {
            offset = reader.ReadInt32();
        }
    }
}