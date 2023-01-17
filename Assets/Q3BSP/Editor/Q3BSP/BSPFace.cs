using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal class BSPFace
    {
        public enum Type : int
        {
            Polygon = 1,
            Patch = 2,
            Mesh = 3,
            Billboard = 4
        }

        public const int LENGTH = 26 * 4;
        
        public int texture { get; set; }
        public int effect { get; set; }
        public Type type { get; set; }
        public int vertex { get; set; }
        public int numVertexes { get; set; }
        public int meshVert { get; set; }
        public int numMeshVerts { get; set; }
        public int lightmapIndex { get; set; }
        public RectInt lightmapCoords { get; set; }
        public Vector3 lightmapOrigin { get; set; }
        public Vector2[] lightmapST { get; set; } = new Vector2[3];
        public Vector3 normal { get; set; }
        public Vector2 size { get; set; }

        public BSPFace(BinaryReader reader)
        {
            texture = reader.ReadInt32();
            effect = reader.ReadInt32();
            type = (Type)reader.ReadInt32();
            vertex = reader.ReadInt32();
            numVertexes = reader.ReadInt32();
            meshVert = reader.ReadInt32();
            numMeshVerts = reader.ReadInt32();

            lightmapIndex = reader.ReadInt32();
            var lmStart = reader.ReadVector2Int();
            var lmSize = reader.ReadVector2Int();
            lightmapCoords = new RectInt(lmStart, lmSize);
            lightmapOrigin = reader.ReadVector3Pos();
            lightmapST[0] = reader.ReadVector2();
            lightmapST[1] = reader.ReadVector2();
            lightmapST[2] = reader.ReadVector2();

            normal = reader.ReadVector3Dir();
            size = CoordinateSpaceUtility.TransformQuakeToUnitySize(reader.ReadVector2Int());
        }
    }
}