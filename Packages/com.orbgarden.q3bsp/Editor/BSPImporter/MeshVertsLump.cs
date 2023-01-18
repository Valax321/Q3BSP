using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class MeshVertsLump : BSPLump
    {
        public IReadOnlyList<BSPMeshVert> meshVerts => m_MeshVerts;

        private List<BSPMeshVert> m_MeshVerts = new();

        public MeshVertsLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPMeshVert.LENGTH)
            {
                m_MeshVerts.Add(new BSPMeshVert(reader));
            }
            
            Debug.Assert(m_MeshVerts.Count == directoryEntry.length / BSPMeshVert.LENGTH);
        }
    }
}