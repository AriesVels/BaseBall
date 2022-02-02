using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generic.MathfUtility
{
    public static class Vector3Util
    {
        public static Vector3 LerpWaypoints(List<Transform> _waypoints, float _single)
        {
            Vector3 pos;
            Quaternion qt;
            Vector3 scale;
            MathfUtil.LerpWaypoints(_waypoints, _single, out pos, out qt, out scale);
            return pos;
        }

        public static Vector3 BezierCurve(Vector3 _start, Vector3 _end, Vector3 _control, float _single)
        {
            var pos1 = Vector3.Lerp(_start, _control, _single);
            var pos2 = Vector3.Lerp(_control, _end, _single);
            return Vector3.Lerp(pos1, pos2, _single);
        }
    }

}