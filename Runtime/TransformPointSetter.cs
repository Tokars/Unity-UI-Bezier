using OT.Attributes;
using OT.Extensions;
using UnityEngine;

namespace UI.Bezier
{
    public abstract class BaseTransformPointSetter<TLine> : MonoBehaviour where TLine : CustomLine
    {
        [SerializeField] protected TLine line;

        [SerializeField] protected RectTransform rect;

        // todo: think about package own camera.
        [field: SerializeField] public Camera Cam { set; get; }

        [SerializeField] protected TransformPointEditor[] trPoints;

        [EditorButton]
        protected virtual void GrabEditorPoints()
        {
            trPoints = GetComponentsInChildren<TransformPointEditor>();
        }
        protected void ZoomCamera(Vector3 camPosInit)
        {
            var camPos = camPosInit;
            camPos.z = -10;
            Cam.transform.position = camPos;
        }

        protected Vector2 ToScreenPos(Vector3 pos)
        {
            Vector2 res;
            Cam.WorldRectToScreenSpace(pos, rect, out res);
            return res;
        }

        [EditorButton]
        public abstract void Draw();

    }


    public class TransformPointSetter : BaseTransformPointSetter<UIMeshLine>
    {
        public override void Draw()
        {
            var camPosInit = Cam.transform.position;
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
            Cam.transform.position = camPosInit;
        }

        
        [EditorButton]
        private void ResetTangents()
        {
            line.ResetTangents();
        }
       
    }
}