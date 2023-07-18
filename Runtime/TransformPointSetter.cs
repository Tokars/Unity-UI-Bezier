using OT.Attributes;
using OT.Extensions;
using UnityEngine;

namespace UI.Bezier
{
    public class TransformPointSetter : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private RectTransform rect;
        [SerializeField] private Transform[] trPoints;
        [SerializeField] private UIMeshLine line;

        [EditorButton]
        private void Start()
        {
            var camPosInit = cam.transform.position;
            ZoomCamera(camPosInit);
            
            int i = 0;
            if (trPoints.Length == 0) return;

            for (; i < line.Points.Count; i++)
            {
                LinePoint point = line.Points[i];
                point.point = ToScreenPos(trPoints[i].transform.position);
                point.prvCurveOffset = point.prvCurveOffset;
                point.nextCurveOffset = point.nextCurveOffset;

                line.Points[i] = point;
            }

            if (trPoints.Length > i)
                for (; i < trPoints.Length; i++)
                    line.AddPoint(new LinePoint(ToScreenPos(trPoints[i].transform.position)));
            
            line.LocalUpdateGeometry();
            cam.transform.position = camPosInit;
        }
        void ZoomCamera(Vector3 camPosInit)
        {
            var camPos = camPosInit; 
            camPos.z = -10;
            cam.transform.position = camPos;
        }
        private Vector2 ToScreenPos(Vector3 pos)
        {
            Vector2 res;
            cam.WorldRectToScreenSpace(pos, rect, out res);
            return res;
        }

    }
}