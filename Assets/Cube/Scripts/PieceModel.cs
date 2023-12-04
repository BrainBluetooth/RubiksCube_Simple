#pragma warning disable IDE0002
#pragma warning disable IDE0003
#pragma warning disable IDE0011
using System;
using UnityEngine;

namespace RubiksCube
{
    public class PieceModel : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public CubeModel Cube { get; private set; }
        public Face Colors { get; private set; }
        public AxisModel AttachedAxis { get; private set; }

        public Vector3 LocalDirection => this.Transform.localPosition.normalized;

        public void Init(CubeModel cube)
        {
            this.Transform = base.transform;

            if (this.Transform.parent != cube.Transform)
                throw new ArgumentException("", nameof(cube));
            this.Cube = cube;

            this.Colors = this.LocalDirection.GetFaces();
            foreach (var viewer in GetComponentsInChildren<FaceViewer>())
            {
                viewer.Init(cube, this.Colors);
            }
        }

        public bool CanAttach => this.AttachedAxis is null;
        public bool TryAttach(AxisModel axis)
        {
            if (!CanAttach)
                return false;
            this.AttachedAxis = axis;
            Transform.SetParent(axis.Transform);
            return true;
        }
        public void Detach()
        {
            Transform.SetParent(Cube.Transform);
            this.AttachedAxis = null;
        }

        private void OnDestroy()
        {
            this.Transform = null;
            this.Cube = null;
            this.AttachedAxis = null;
        }
    }
}