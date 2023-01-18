using System.IO;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class VisdataLump : BSPLump
    {
        public int numVecs { get; }
        public int sizeOfVec { get; }
        public byte[] vecs { get; }

        public VisdataLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            numVecs = reader.ReadInt32();
            sizeOfVec = reader.ReadInt32();
            vecs = new byte[numVecs * sizeOfVec];
            for (var i = 0; i < vecs.Length; i++)
            {
                vecs[i] = reader.ReadByte();
            }
        }
    }
}