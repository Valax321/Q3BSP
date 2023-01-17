using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPNode
    {
        public const int LENGTH = 9 * 4;
        
        public int plane { get; set; }
        public int[] children { get; set; } = new int[2];
        public Bounds bounds { get; set; }

        public BSPNode(BinaryReader reader)
        {
            plane = reader.ReadInt32();
            children[0] = reader.ReadInt32();
            children[1] = reader.ReadInt32();

            var minsx = reader.ReadInt32();
            var minsy = reader.ReadInt32();
            var minsz = reader.ReadInt32();

            var maxsx = reader.ReadInt32();
            var maxsy = reader.ReadInt32();
            var maxsz = reader.ReadInt32();
            
            bounds.SetMinMax(
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(minsx, minsy, minsz)), 
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(maxsx, maxsy, maxsz)));
        }
    }
}