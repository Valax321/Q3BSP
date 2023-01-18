using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class LeafsLump : BSPLump
    {
        public IReadOnlyList<BSPLeaf> leafs => m_Leafs;

        private List<BSPLeaf> m_Leafs = new();

        public LeafsLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPLeaf.LENGTH)
            {
                m_Leafs.Add(new BSPLeaf(reader));
            }
            
            Debug.Assert(m_Leafs.Count == directoryEntry.length / BSPLeaf.LENGTH);
        }
    }
}