using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BrushSidesLump : BSPLump
    {
        public IReadOnlyList<BSPBrushSide> brushSides => m_BrushSides;

        private List<BSPBrushSide> m_BrushSides = new();

        public BrushSidesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPBrushSide.LENGTH)
            {
                m_BrushSides.Add(new BSPBrushSide(reader));
            }
            
            Debug.Assert(m_BrushSides.Count == directoryEntry.length / BSPBrushSide.LENGTH);
        }
    }
}