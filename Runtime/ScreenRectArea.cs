using OT.Attributes;
using OT.Extensions;
using UnityEngine;

namespace UI.Bezier
{
    public class ScreenRectArea : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private RectTransform area;
        [SerializeField] private UIMeshLine line;
        [SerializeField] private Transform[] childTrPoints;
        [SerializeField] private Vector2[] positions;
        [SerializeField] private LinePoint[] points;

        
        

        [EditorButton]
        private void CalcPoints()
        {
            var camPosInit = cam.transform.position;
            ZoomCamera(camPosInit);
            childTrPoints = GetComponentsInChildren<Transform>();
            positions = new Vector2[childTrPoints.Length];
            points = new LinePoint[childTrPoints.Length];
            for (int i = 0; i < childTrPoints.Length; i++)
            {
                cam.WorldRectToScreenSpace(childTrPoints[i].position, area, out positions[i]);
                print($"positions[{i}] = {positions[i]}");
                points[i] = new LinePoint(positions[i]);
            }
            cam.transform.position = camPosInit;
        }

        void ZoomCamera(Vector3 camPosInit)
        {
            var camPos = camPosInit; 
            camPos.z = -10;
            cam.transform.position = camPos;
        }

        [EditorButton]
        private void ApplyToLine()
        {
            line.SetPoints(points);
        }
    }
}