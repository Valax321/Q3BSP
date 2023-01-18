using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class EffectsLump : BSPLump
    {
        public IReadOnlyList<BSPEffect> effects => m_Effects;

        private List<BSPEffect> m_Effects = new();

        public EffectsLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPEffect.LENGTH)
            {
                m_Effects.Add(new BSPEffect(reader));
            }
            
            Debug.Assert(m_Effects.Count == directoryEntry.length / BSPEffect.LENGTH);
        }
    }
}