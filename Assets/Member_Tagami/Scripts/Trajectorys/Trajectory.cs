using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public bool strike { private set; get; }

    protected void StrikeAndDestroy(BallController _ball)
    {
        if (!_ball.IsHit())
        {
            strike = true;
            Destroy(_ball.gameObject);
        }
    }

    public void ThrowTrajectory(BallController _ball)
    {
        strike = false;
        OnThrow(_ball);
    }

    protected virtual void OnThrow(BallController _ball) { Debug.LogError("Override‚µ‚Ä‚­‚¾‚³‚¢"); }
}
