using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generic.MathfUtility
{
    public static class MathfUtil
    {
        public static float DistanceWaypoints(List<Transform> _waypoints)
        {
            float distance = 0.0f;
            for (int i = 0; i < _waypoints.Count - 1; i++)
            {
                distance += Vector3.Distance(_waypoints[i].position, _waypoints[i + 1].position);
            }
            return distance;
        }

        public static void LerpWaypoints(List<Transform> _waypoints, float _single, out Vector3 _positon, out Quaternion _rotation, out Vector3 _localScale)
        {
            //初期化
            _positon = Vector3.zero;
            _rotation = Quaternion.identity;
            _localScale = Vector3.one;

            //エラー処理
            if (_waypoints.Count < 2)
            {
                Debug.LogError("Waypointは必ず２つ以上設定してください");
                return;
            }
            //01の間にする
            _single = Mathf.Clamp01(_single);

            //Singleの位置を超える場所を探す
            var totalDistance = DistanceWaypoints(_waypoints);
            var singleDistance = totalDistance * _single;
            float sumDistance = 0.0f;
            for (int i = 0; i < _waypoints.Count - 1; i++)
            {
                float waypointDistance = Vector3.Distance(_waypoints[i].position, _waypoints[i + 1].position);
                if (sumDistance + waypointDistance >= singleDistance)
                {
                    var dt = (singleDistance - sumDistance) / waypointDistance;

                    _positon = Vector3.Lerp(_waypoints[i].position, _waypoints[i + 1].position, dt);
                    _rotation = Quaternion.Slerp(_waypoints[i].rotation, _waypoints[i + 1].rotation, dt);
                    _localScale = Vector3.Lerp(_waypoints[i].position, _waypoints[i + 1].position, dt);
                    return;

                }

                //実際に足す
                sumDistance += Vector3.Distance(_waypoints[i].position, _waypoints[i + 1].position);
            }
        }

    }
}