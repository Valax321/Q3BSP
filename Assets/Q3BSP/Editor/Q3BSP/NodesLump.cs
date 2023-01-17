using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class NodesLump : BSPLump
    {
        public IReadOnlyList<BSPNode> nodes => m_Nodes;

        private List<BSPNode> m_Nodes = new();

        public NodesLump(BinaryReader reader, ref BSPDirectoryEntry directoryEntry) : base(reader, ref directoryEntry)
        {
            for (var i = 0; i < directoryEntry.length; i += BSPNode.LENGTH)
            {
                m_Nodes.Add(new BSPNode(reader));
            }
            
            Debug.Assert(m_Nodes.Count == directoryEntry.length / BSPNode.LENGTH);
        }
    }
}