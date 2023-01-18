using System.IO;
using System.Text;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class EntitiesLump : BSPLump
    {
        
        
        public EntitiesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            var entString = Encoding.UTF8.GetString(reader.ReadBytes(directoryEntry.length));
            //entityData = new EntityData(entString);
        }
    }
}