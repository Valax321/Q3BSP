using System.IO;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal abstract class BSPLump
    {
        protected BSPLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry)
        {
            reader.BaseStream.Seek(directoryEntry.offset, SeekOrigin.Begin);
        }
    }
}