using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class PlanesLump : BSPLump
    {
        public IReadOnlyList<BSPPlane> planes => m_Planes;

        private List<BSPPlane> m_Planes = new();

        public PlanesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPPlane.LENGTH)
            {
                m_Planes.Add(new BSPPlane(reader));
            }
            
            Debug.Assert(m_Planes.Count == directoryEntry.length / BSPPlane.LENGTH);
        }
    }
}