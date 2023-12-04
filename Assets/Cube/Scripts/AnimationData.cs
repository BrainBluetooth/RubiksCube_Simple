#pragma warning disable IDE0044
using System;
using UnityEngine;

namespace RubiksCube
{
    [Serializable]
    public class AnimationData
    {
        [SerializeField] private float m_duration;

        public float Duration => m_duration;
    }
}