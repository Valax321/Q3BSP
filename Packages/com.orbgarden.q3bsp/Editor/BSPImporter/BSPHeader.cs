using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal struct BSPHeader
    {
        private const int DIRENTRY_COUNT = 17;
        
        public uint magic { get; }
        public int version { get; set; }
        public BSPDirectoryEntry[] directories { get; }
        
        public BSPHeader(BinaryReader reader)
        {
            directories = new BSPDirectoryEntry[DIRENTRY_COUNT];
            magic = reader.ReadUInt32();
            version = reader.ReadInt32();
            for (var i = 0; i < DIRENTRY_COUNT; i++)
            {
                directories[i] = new BSPDirectoryEntry(reader);
            }
        }
    }
}