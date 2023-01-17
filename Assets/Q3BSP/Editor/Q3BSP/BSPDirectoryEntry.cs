using System.IO;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPDirectoryEntry
    {
        public int offset { get; set; }
        public int length { get; set; }
        
        public BSPDirectoryEntry(BinaryReader reader)
        {
            offset = reader.ReadInt32();
            length = reader.ReadInt32();
        }
    }
}