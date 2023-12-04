#pragma warning disable IDE0002
#pragma warning disable IDE0003
using DG.Tweening;
using System;
using UnityEngine;

namespace RubiksCube
{
    [RequireComponent(typeof(RotateModel))]
    public class CubeModel : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public RotateModel RotateModel { get; private set; }
        [SerializeField] private Color m_leftColor; public Color LeftColor => m_leftColor;
        [SerializeField] private Color m_rightColor; public Color RightColor => m_rightColor;
        [SerializeField] private Color m_upColor; public Color UpColor => m_upColor;
        [SerializeField] private Color m_downColor; public Color DownColor => m_downColor;
        [SerializeField] private Color m_forwardColor; public Color ForwardColor => m_forwardColor;
        [SerializeField] private Color m_backColor; public Color BackColor => m_backColor;

        // init
        private void Awake()
        {
            this.Transform = base.transform;
            this.RotateModel = base.GetComponent<RotateModel>();
            this.RotateModel.Init(this);
        }

        public bool CanRotateFace(Func<AxisModel, bool> predicate)
        {
            return RotateModel.CanRotate(predicate, out _);
        }
        public bool TryRotateFace(Func<AxisModel, bool> predicate, bool senseOfRotation)
        {
            return RotateModel.TryRotate(predicate, senseOfRotation);
        }

        [SerializeField] private AnimationData m_cubeRotateAnim;
        private Tween m_tween;
        private Vector3 m_cubeRotateAxis;

        public bool CanRotateCube(Vector3 axis)
        {
            if (m_tween is null)
                return true;
            float cos = Util.Cos(axis, m_cubeRotateAxis);
            float abs = Mathf.Abs(cos);
            return abs > 0.5f;
        }
        public bool TryRotateCube(Vector3 axis)
        {
            if (CanRotateCube(axis))
            {
                m_cubeRotateAxis = axis;
                m_tween = this.Transform.DOBlendableLocalRotateBy(axis * 90f, m_cubeRotateAnim.Duration);
                m_tween.onComplete += () =>
                {
                    if (m_tween.IsComplete())
                        m_tween = null;
                };
                return true;
            }
            return false;
        }
    }
}