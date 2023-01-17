using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class LightmapsLump : BSPLump
    {
        public IReadOnlyList<BSPLightmap> lightmaps => m_Lightmaps;

        private List<BSPLightmap> m_Lightmaps = new();

        public LightmapsLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            // No lightmaps! Might be external
            if (directoryEntry.length == 0)
                return;
            
            for (var i = 0; i < directoryEntry.length; i += BSPLightmap.LENGTH)
            {
                m_Lightmaps.Add(new BSPLightmap(reader));
            }
            
            Debug.Assert(m_Lightmaps.Count == directoryEntry.length / BSPLightmap.LENGTH);
        }
    }
}