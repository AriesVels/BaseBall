using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generic.MathfUtility
{
    public static class QuaternionUtil
    {
        public static Quaternion BezierCurve(Quaternion _start, Quaternion _end, Quaternion _control, float _single)
        {
            return Quaternion.Slerp(Quaternion.Slerp(_start, _control, _single), Quaternion.Slerp(_control, _end, _single), _single);
        }
    }
}
