using System;
using UnityEngine;

namespace RubiksCube.Example
{
    public class CubeController : MonoBehaviour
    {
        public float timeScale;

        [SerializeField] private CubeModel m_cube;
        [SerializeField] private AudioClip clip1;
        [SerializeField] private AudioClip clip2;

        private static Face WorldDirToViewFace(Vector3 wDir)
        {
            Vector3 vDir = Camera.main.worldToCameraMatrix.MultiplyVector(wDir);
            Face vFace = vDir.GetFace();
            return vFace;
        }

        private bool TryAction(KeyCode key, Func<bool> act)
        {
            return Input.GetKeyDown(key) && act();
        }

        private bool TryRotateFace(Face face, bool opposite)
        {
            bool flag = m_cube.TryRotateFace((AxisModel axis) => WorldDirToViewFace(axis.WorldDirection) == face, opposite);
            if (flag && clip1)
                AudioSource.PlayClipAtPoint(clip1, Camera.main.transform.position);
            return flag;
        }

        private bool TryRotateCube(Vector3 axis)
        {
            bool flag = m_cube.TryRotateCube(axis);
            if (flag && clip2)
                AudioSource.PlayClipAtPoint(clip2, Camera.main.transform.position);
            return flag;
        }

        private void Update()
        {
            Time.timeScale = timeScale;

            bool opposite = Input.GetKey(KeyCode.Space);
            TryAction(KeyCode.A, () => TryRotateFace(Face.Left, opposite));
            TryAction(KeyCode.D, () => TryRotateFace(Face.Right, opposite));
            TryAction(KeyCode.S, () => TryRotateFace(Face.Down, opposite));
            TryAction(KeyCode.W, () => TryRotateFace(Face.Up, opposite));
            TryAction(KeyCode.Q, () => TryRotateFace(Face.Back, opposite));
            TryAction(KeyCode.E, () => TryRotateFace(Face.Forward, opposite));

            TryAction(KeyCode.LeftArrow, () => TryRotateCube(Vector3.up));
            TryAction(KeyCode.RightArrow, () => TryRotateCube(Vector3.down));
            TryAction(KeyCode.DownArrow, () => TryRotateCube(Vector3.left));
            TryAction(KeyCode.UpArrow, () => TryRotateCube(Vector3.right));
        }

        private void OnDestroy()
        {
            m_cube = null;
            clip1 = null;
            clip2 = null;
        }
    }
}