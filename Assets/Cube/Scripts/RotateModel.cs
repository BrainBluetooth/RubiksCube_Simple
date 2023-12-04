#pragma warning disable IDE0044
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RubiksCube
{
    public class RotateModel : MonoBehaviour
    {
        public CubeModel CubeModel { get; private set; }
        private PieceModel[] m_pieces;
        private AxisModel[] m_axises;
        private List<PieceModel> m_temp;

        public void Init(CubeModel cube)
        {
            this.CubeModel = cube;
            m_temp = new List<PieceModel>(9);
            m_axises = GetComponentsInChildren<AxisModel>();
            foreach (var axis in m_axises)
                axis.Init(this);
            m_pieces = GetComponentsInChildren<PieceModel>();
            foreach (var piece in m_pieces)
                piece.Init(cube);
        }

        public bool CanRotate(Func<AxisModel, bool> predicate, out AxisModel axis)
        {
            axis = m_axises.First(predicate);
            if (!axis.CanRotate)
                return false;
            m_temp.Clear();
            foreach (var piece in m_pieces)
            {
                if (piece.CanAttach)
                {
                    float cs = Util.Cos(axis.LocalDirection, piece.LocalDirection);
                    if (cs > 0.5f)
                        m_temp.Add(piece);
                }
            }
            return m_temp.Count == 9;
        }
        public bool TryRotate(Func<AxisModel, bool> predicate, bool opposite)
        {
            if (CanRotate(predicate, out var axis))
            {
                foreach (var piece in m_temp)
                    axis.Attach(piece);
                axis.TryRotate(opposite);
                return true;
            }
            return false;
        }

        private void OnDestroy()
        {
            this.CubeModel = null;
            m_pieces = null;
            m_axises = null;
            m_temp.Clear();
            m_temp = null;
        }
    }
}