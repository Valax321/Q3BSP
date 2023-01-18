using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal abstract class BSPLump
    {
        protected BSPLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry)
        {
            reader.BaseStream.Seek(directoryEntry.offset, SeekOrigin.Begin);
        }
    }
}