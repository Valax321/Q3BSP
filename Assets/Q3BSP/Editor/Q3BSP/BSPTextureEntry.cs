using System.IO;
using System.Text;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPTextureEntry
    {
        public const int TEXTURE_STRING_LENGTH = 64;
        public const int LENGTH = 64 + 4 + 4; // 64 chars + 2x 32 bit ints
        
        public string name { get; }
        public int flags { get; }
        public int contents { get; }

        public BSPTextureEntry(BinaryReader reader)
        {
            name = Encoding.UTF8.GetString(reader.ReadBytes(TEXTURE_STRING_LENGTH));
            flags = reader.ReadInt32();
            contents = reader.ReadInt32();
        }
    }
}