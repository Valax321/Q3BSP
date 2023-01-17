using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class LeafFacesLump : BSPLump
    {
        public IReadOnlyList<BSPLeafFace> leafFaces => m_Faces;

        private List<BSPLeafFace> m_Faces = new();

        public LeafFacesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPLeafFace.LENGTH)
            {
                m_Faces.Add(new BSPLeafFace(reader));
            }
            
            Debug.Assert(m_Faces.Count == directoryEntry.length / BSPLeafFace.LENGTH);
        }
    }
}