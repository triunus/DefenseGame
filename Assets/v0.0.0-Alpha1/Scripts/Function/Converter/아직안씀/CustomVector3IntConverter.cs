using System;
using UnityEngine;

namespace Function.PositionConverter
{
    public class CustomVector3IntConverter
    {
        public CustomVector3Int Vector3ToCustomVector3Int(Vector3 vector3)
        {
            return new CustomVector3Int(Convert.ToInt32(vector3.x), Convert.ToInt32(vector3.y), Convert.ToInt32(vector3.z));
        }
        public CustomVector3Int Vector3IntToCustomVector3Int(Vector3Int vector3Int)
        {
            return new CustomVector3Int(vector3Int.x, vector3Int.y, vector3Int.z);
        }
        public Vector3 CustomVector3IntToVector3(CustomVector3Int customVector3Int)
        {
            return new Vector3(customVector3Int.X, customVector3Int.Y, customVector3Int.Z);
        }
        public Vector3Int CustomVector3IntToVector3Int(CustomVector3Int customVector3Int)
        {
            return new Vector3Int(customVector3Int.X, customVector3Int.Y, customVector3Int.Z);
        }
    }

    public struct CustomVector3Int
    {
        private int x;
        private int y;
        private int z;

        public CustomVector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Z { get => z; set => z = value; }
    }
}
