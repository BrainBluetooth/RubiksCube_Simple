#pragma warning disable IDE0011
using System;
using UnityEngine;

namespace RubiksCube
{
    public static class Util
    {
        public static float Cos(Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a.normalized, b.normalized);
        }

        // Used in Axis
        public static Face GetFace(this Vector3 dir)
        {
            Vector3 abs = new Vector3(Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z));
            float max = abs.x;
            Face face = Face.Right;
            if (max < abs.y)
            {
                max = abs.y;
                face = Face.Up;
            }
            if (max < abs.z)
                face = Face.Forward;
            switch (face)
            {
                case Face.Right:
                    return dir.x > 0 ? Face.Right : Face.Left;
                case Face.Up:
                    return dir.y > 0 ? Face.Up : Face.Down;
                case Face.Forward:
                    return dir.z > 0 ? Face.Forward : Face.Back;
            }
            return Face.None;
        }
        // Used In Piece
        public static Face GetFaces(this Vector3 dir, float crit = 0.5f)
        {
            if (crit < 0f)
                throw new ArgumentOutOfRangeException(nameof(crit), crit, $"{nameof(crit)} is negative");
            Face faces = Face.None;
            //dir.Normalize();
            if (dir.x > crit) faces |= Face.Right;
            else if (dir.x < -crit) faces |= Face.Left;
            if (dir.y > crit) faces |= Face.Up;
            else if (dir.y < -crit) faces |= Face.Down;
            if (dir.z > crit) faces |= Face.Forward;
            else if (dir.z < -crit) faces |= Face.Back;
            return faces;
        }
    }
}