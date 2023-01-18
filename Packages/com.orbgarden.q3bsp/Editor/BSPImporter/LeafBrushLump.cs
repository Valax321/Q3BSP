using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class LeafBrushLump : BSPLump
    {
        public IReadOnlyList<BSPLeafBrush> LeafBrushes => m_Brushes;

        private List<BSPLeafBrush> m_Brushes = new();

        public LeafBrushLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPLeafFace.LENGTH)
            {
                m_Brushes.Add(new BSPLeafBrush(reader));
            }
            
            Debug.Assert(m_Brushes.Count == directoryEntry.length / BSPLeafBrush.LENGTH);
        }
    }
}