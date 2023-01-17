using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class FacesLump : BSPLump
    {
        public IReadOnlyList<BSPFace> faces => m_Faces;

        private List<BSPFace> m_Faces = new();

        public FacesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPFace.LENGTH)
            {
                m_Faces.Add(new BSPFace(reader));
            }
            
            Debug.Assert(m_Faces.Count == directoryEntry.length / BSPFace.LENGTH);
        }
    }
}