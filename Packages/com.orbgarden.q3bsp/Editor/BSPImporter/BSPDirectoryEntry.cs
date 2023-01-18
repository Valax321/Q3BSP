using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
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