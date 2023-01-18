using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class BrushesLump : BSPLump
    {
        public IReadOnlyList<BSPBrush> brushes => m_Brushes;

        private List<BSPBrush> m_Brushes = new();

        public BrushesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPBrush.LENGTH)
            {
                m_Brushes.Add(new BSPBrush(reader));
            }
            
            Debug.Assert(m_Brushes.Count == directoryEntry.length / BSPBrush.LENGTH);
        }
    }
}