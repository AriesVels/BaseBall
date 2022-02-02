using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcVelocity : MonoBehaviour
{
    // 1�t���[���O�̈ʒu
    private Vector3 prevPosition;

    private Vector3 velocity;

    private void Start()
    {
        // �����ʒu��ێ�
        prevPosition = transform.position;
    }

    private void Update()
    {
        // deltaTime��0�̏ꍇ�͉������Ȃ�
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        // ���݈ʒu�擾
        var position = transform.position;

        // ���ݑ��x�v�Z
        velocity = (position - prevPosition) / Time.deltaTime;

        // �O�t���[���ʒu���X�V
        prevPosition = position;
    }

    public Vector3 GetVelocity() { return velocity; }
}
