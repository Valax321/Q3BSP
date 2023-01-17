using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class LightVolsLump : BSPLump
    {
        public IReadOnlyList<BSPLightVol> lightVols => m_LightVols;

        private List<BSPLightVol> m_LightVols = new();

        public LightVolsLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPLightVol.LENGTH)
            {
                m_LightVols.Add(new BSPLightVol(reader));
            }
            
            Debug.Assert(m_LightVols.Count == directoryEntry.length / BSPLightVol.LENGTH);
        }
    }
}