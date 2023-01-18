using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class ModelsLump : BSPLump
    {
        public IReadOnlyList<BSPModel> models => m_Models;

        private List<BSPModel> m_Models = new();

        public ModelsLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPModel.LENGTH)
            {
                m_Models.Add(new BSPModel(reader));
            }
            
            Debug.Assert(m_Models.Count == directoryEntry.length / BSPModel.LENGTH);
        }
    }
}