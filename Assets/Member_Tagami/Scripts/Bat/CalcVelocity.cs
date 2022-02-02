using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcVelocity : MonoBehaviour
{
    // 1フレーム前の位置
    private Vector3 prevPosition;

    private Vector3 velocity;

    private void Start()
    {
        // 初期位置を保持
        prevPosition = transform.position;
    }

    private void Update()
    {
        // deltaTimeが0の場合は何もしない
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        // 現在位置取得
        var position = transform.position;

        // 現在速度計算
        velocity = (position - prevPosition) / Time.deltaTime;

        // 前フレーム位置を更新
        prevPosition = position;
    }

    public Vector3 GetVelocity() { return velocity; }
}
