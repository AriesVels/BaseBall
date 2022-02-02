using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory_WayPoints : Trajectory
{
    [SerializeField] List<Transform> wayPoints = new List<Transform>();

    float lerpTimer;
    [SerializeField] float lerpSeconds = 1;

    BallController ball;

    [Header("Debug")]
    [SerializeField] bool drawGizmos;

    void Update()
    {
        if (ball)
        {
            lerpTimer += Time.deltaTime;
            if (lerpTimer > lerpSeconds)
            {
                lerpTimer = lerpSeconds;
                StrikeAndDestroy(ball);
            }
            if (ball)
            {
                ball.SetPosition(Generic.MathfUtility.Vector3Util.LerpWaypoints(wayPoints, lerpTimer / lerpSeconds));
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (wayPoints.Count >= 2 && drawGizmos)
        {
            for (int i = 0; i < (wayPoints.Count - 1); i++)
            {
                Gizmos.DrawLine(wayPoints[i].position, wayPoints[i + 1].position);
            }
        }
    }

    protected override void OnThrow(BallController _ball)
    {
        ball = _ball;
        lerpTimer = 0;
    }
}
