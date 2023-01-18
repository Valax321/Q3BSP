using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class BSPLeaf
    {
        public const int LENGTH = 12 * 4; // 12x int32s
        
        public int cluster { get; set; }
        public int area { get; set; }
        public Bounds bounds { get; set; }
        public int leafFace { get; set; }
        public int numLeafFaces { get; set; }
        public int leafBrush { get; set; }
        public int numLeafBrushes { get; set; }

        public BSPLeaf(BinaryReader reader)
        {
            cluster = reader.ReadInt32();
            area = reader.ReadInt32();
            
            var minsx = reader.ReadInt32();
            var minsy = reader.ReadInt32();
            var minsz = reader.ReadInt32();

            var maxsx = reader.ReadInt32();
            var maxsy = reader.ReadInt32();
            var maxsz = reader.ReadInt32();
            
            bounds.SetMinMax(
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(minsx, minsy, minsz)), 
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(maxsx, maxsy, maxsz)));

            leafFace = reader.ReadInt32();
            numLeafFaces = reader.ReadInt32();

            leafBrush = reader.ReadInt32();
            numLeafBrushes = reader.ReadInt32();
        }
    }
}