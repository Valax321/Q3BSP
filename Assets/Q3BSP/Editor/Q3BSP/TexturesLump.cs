using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class TexturesLump : BSPLump
    {
        public IReadOnlyList<BSPTextureEntry> entries => m_Entries;

        private List<BSPTextureEntry> m_Entries = new();
        
        public TexturesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPTextureEntry.LENGTH)
            {
                m_Entries.Add(new BSPTextureEntry(reader));
            }
            
            Debug.Assert(m_Entries.Count == directoryEntry.length / BSPTextureEntry.LENGTH);
        }
    }
}