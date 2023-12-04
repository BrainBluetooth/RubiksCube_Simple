#pragma warning disable IDE0002
#pragma warning disable IDE0003
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace RubiksCube
{
    public class AxisModel : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public RotateModel RotateModel { get; private set; }
        private List<PieceModel> m_attached;

        public Vector3 LocalDirection => this.Transform.localPosition.normalized;
        public Vector3 WorldDirection => this.Transform.localToWorldMatrix.MultiplyVector(LocalDirection);

        public void Init(RotateModel rotate)
        {
            this.Transform = base.transform;
            this.RotateModel = rotate;
            m_attached = new List<PieceModel>(9);
        }

        [SerializeField] private AnimationData m_faceRotateAnim;
        private Tween m_tween;

        public bool CanRotate => m_tween is null;
        public bool TryRotate(bool opposite)
        {
            if (CanRotate)
            {
                m_tween = this.Transform.DOBlendableLocalRotateBy(LocalDirection * 90 * (opposite ? -1 : 1), m_faceRotateAnim.Duration);
                m_tween.onComplete += () =>
                {
                    DetachAll();
                    m_tween = null;
                };
                return true;
            }
            return false;
        }

        public void Attach(PieceModel piece)
        {
            piece.TryAttach(this);
            m_attached.Add(piece);
        }
        private void DetachAll()
        {
            foreach (var piece in m_attached)
                piece.Detach();
            m_attached.Clear();
        }

        private void OnDestroy()
        {
            Transform = null;
            RotateModel = null;
            m_attached.Clear();
            m_attached = null;
        }
    }
}