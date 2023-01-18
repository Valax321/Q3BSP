using System.IO;
using UnityEngine;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class BSPModel
    {
        public const int LENGTH = 10 * 4;
        
        public Bounds bounds { get; set; }
        public int face { get; set; }
        public int numFaces { get; set; }
        public int brush { get; set; }
        public int numBrushes { get; set; }

        public BSPModel(BinaryReader reader)
        {
            var minsx = reader.ReadInt32();
            var minsy = reader.ReadInt32();
            var minsz = reader.ReadInt32();

            var maxsx = reader.ReadInt32();
            var maxsy = reader.ReadInt32();
            var maxsz = reader.ReadInt32();
            
            bounds.SetMinMax(
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(minsx, minsy, minsz)), 
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(maxsx, maxsy, maxsz)));

            face = reader.ReadInt32();
            numFaces = reader.ReadInt32();
            brush = reader.ReadInt32();
            numBrushes = reader.ReadInt32();
        }
    }
}