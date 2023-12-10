using UnityEngine;

namespace RubiksCube
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FaceViewer : MonoBehaviour
    {
        [SerializeField] private Face m_face;

        private static Color GetColor(CubeModel cube, Face face)
        {
            Color c = Color.black;
            switch (face)
            {
                case Face.Right:
                    c = cube.RightColor;
                    break;
                case Face.Left:
                    c = cube.LeftColor;
                    break;
                case Face.Up:
                    c = cube.UpColor;
                    break;
                case Face.Down:
                    c = cube.DownColor;
                    break;
                case Face.Forward:
                    c = cube.ForwardColor;
                    break;
                case Face.Back:
                    c = cube.BackColor;
                    break;
            }
            return c;
        }

        public void Init(CubeModel cube, Face colors)
        {
            Material mat = GetComponent<MeshRenderer>().material;
            Face face = m_face;
            Color c = Color.black;
            if ((face & colors) != 0)
                c = GetColor(cube, face);
            mat.color = c;
        }
    }
}