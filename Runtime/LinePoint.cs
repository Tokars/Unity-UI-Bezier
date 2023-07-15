using UnityEngine;

namespace UI.Bezier
{
    [System.Serializable]
    public struct LinePoint
    {
        public Vector2 point;
        public bool isNextCurve;
        public Vector2 nextCurveOffset;
        public bool isPrvCurve;
        public Vector2 prvCurveOffset;

        public Vector2 NextCurvePoint
        {
            // get => nextCurveOffset;
            // set => nextCurveOffset = value;

            get => nextCurveOffset + point;
            set => nextCurveOffset = value - point;
        }

        public Vector2 PrvCurvePoint
        {
            // get => prvCurveOffset;
            // set => prvCurveOffset = value;

            get => prvCurveOffset + point;
            set => prvCurveOffset = value - point;
        }

        public void ResetOffsets()
        {
            prvCurveOffset = point;
            nextCurveOffset = point;
        }
        /*public Vector2 NextCurveOffset
        {
            get => nextCurveOffset;
            set => nextCurveOffset = value;
        }

        public Vector2 PrevCurveOffset
        {
            get => prvCurveOffset;
            set => prvCurveOffset = value;
        }*/

        [Range(1, 100)] public ushort nextCurveDivideCount;

        [Range(0, 200)] public float width;

        public float angle;

        public LinePoint(Vector3 p,
            bool nextCurve = false, bool prevCurve = false,
            Vector2 nextCurveOff = default, Vector2 prevCurveOff = default,
            ushort polygons = 12, float w = 10)
        {
            point = p;
            isNextCurve = nextCurve;
            isPrvCurve = prevCurve;

            nextCurveOffset = nextCurveOff;
            prvCurveOffset = prevCurveOff;
            nextCurveDivideCount = polygons;
            width = w;
            angle = 0f;
        }
    }
}