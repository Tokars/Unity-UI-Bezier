using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Bezier
{
    public class TransformPointEditor : UIBehaviour
    {
        [SerializeField, Range(0, 255)] public byte populatePointCount = 6;
        public Vector2 Position => GetComponent<RectTransform>().position;
    }
}