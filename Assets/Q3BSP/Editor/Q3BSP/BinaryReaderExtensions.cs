using System.IO;
using UnityEngine;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    internal static class BinaryReaderExtensions
    {
        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();

            return new Vector2(x, y);
        }

        public static Vector2Int ReadVector2Int(this BinaryReader reader)
        {
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();

            return new Vector2Int(x, y);
        }
        
        public static Vector3 ReadVector3Pos(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();

            return CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(x, y, z));
        }
        
        public static Vector3 ReadVector3Dir(this BinaryReader reader)
        {
            var x = reader.ReadSingle();
            var y = reader.ReadSingle();
            var z = reader.ReadSingle();

            return CoordinateSpaceUtility.TransformQuakeToUnityDirection(new Vector3(x, y, z));
        }

        public static Bounds ReadMinMaxBoundsInt(this BinaryReader reader)
        {
            var bounds = new Bounds();
            
            var minsx = reader.ReadInt32();
            var minsy = reader.ReadInt32();
            var minsz = reader.ReadInt32();

            var maxsx = reader.ReadInt32();
            var maxsy = reader.ReadInt32();
            var maxsz = reader.ReadInt32();
            
            bounds.SetMinMax(
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(minsx, minsy, minsz)), 
                CoordinateSpaceUtility.TransformQuakeToUnityPosition(new Vector3(maxsx, maxsy, maxsz)));

            return bounds;
        }

        public static Color ReadColorRGB(this BinaryReader reader)
        {
            var r = reader.ReadByte();
            var g = reader.ReadByte();
            var b = reader.ReadByte();

            return new Color(r / 256.0f, g / 256.0f, b / 256.0f);
        }
        
        public static Color ReadColorRGBA(this BinaryReader reader)
        {
            var r = reader.ReadByte();
            var g = reader.ReadByte();
            var b = reader.ReadByte();
            var a = reader.ReadByte();

            return new Color(r / 256.0f, g / 256.0f, b / 256.0f, a / 256.0f);
        }
    }
}