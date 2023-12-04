using System;

namespace RubiksCube
{
    [Flags]
    public enum Face
    {
        None = 0,
        Right = 1 << 0,
        Up = 1 << 1,
        Forward = 1 << 2,
        Left = 1 << 3,
        Down = 1 << 4,
        Back = 1 << 5
    }
}