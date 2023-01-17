using System.IO;
using System.Text;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPEffect
    {
        public const int LENGTH = 64 + 2 * 4;
        
        public string name { get; set; }
        public int brush { get; set; }
        public int unknown { get; set; }

        public BSPEffect(BinaryReader reader)
        {
            name = Encoding.UTF8.GetString(reader.ReadBytes(64));
            brush = reader.ReadInt32();
            unknown = reader.ReadInt32();
        }
    }
}