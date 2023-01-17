using System.IO;
using System.Text;
using OrbGarden.TrenchbroomImport.EntityParser;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class EntitiesLump : BSPLump
    {
        public EntityData entityData { get; }
        
        public EntitiesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            var entString = Encoding.UTF8.GetString(reader.ReadBytes(directoryEntry.length));
            entityData = new EntityData(entString);
        }
    }
}