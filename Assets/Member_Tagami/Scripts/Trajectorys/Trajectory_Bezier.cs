using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory_Bezier : Trajectory
{
    [SerializeField] Transform controlPointTransform;
    [SerializeField] float arrivalSeconds = 1;
    float arrivalTimer = 0;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    BallController ball = null;

    // Update is called once per frame
    void Update()
    {
        if (ball)
        {
            bool arrived = false;
            arrivalTimer += Time.deltaTime;
            if (arrivalTimer > arrivalSeconds)
            {
                arrivalTimer = arrivalSeconds;
                arrived = true;
            }
            ball.SetPosition(Generic.MathfUtility.Vector3Util.BezierCurve(startPoint.position, endPoint.position, controlPointTransform.position, arrivalTimer / arrivalSeconds));

            if (arrived)
            {
                StrikeAndDestroy(ball);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (startPoint && endPoint)
        {
            int numPartition = 10;
            for (int i = 0; i < 10; i++)
            {
                var from = Generic.MathfUtility.Vector3Util.BezierCurve(startPoint.position, endPoint.position, controlPointTransform.position, (float)i / numPartition);
                var to = Generic.MathfUtility.Vector3Util.BezierCurve(startPoint.position, endPoint.position, controlPointTransform.position, (float)(i + 1) / numPartition);
                Gizmos.DrawLine(from, to);
            }
        }
    }

    protected override void OnThrow(BallController _ball)
    {
        ball = _ball;
        arrivalTimer = 0.0f;
    }
}
