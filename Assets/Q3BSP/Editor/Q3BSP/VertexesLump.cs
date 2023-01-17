using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class VertexesLump : BSPLump
    {
        public IReadOnlyList<BSPVertex> vertexes => m_Vertexes;

        private List<BSPVertex> m_Vertexes = new();

        public VertexesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPVertex.LENGTH)
            {
                m_Vertexes.Add(new BSPVertex(reader));
            }
            
            Debug.Assert(m_Vertexes.Count == directoryEntry.length / BSPVertex.LENGTH);
        }
    }
}