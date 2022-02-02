using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    bool isHit;

    Rigidbody rb;
    [SerializeField] float velocityMultiplier = 1.0f;
    [SerializeField] string batTag = "Bat";

    //bool canControlTrajectory = true;

    public bool IsHit()
    {
        return isHit;
    }

    public void SetPosition(Vector3 _pos)
    {
        if (!isHit)
        {
            transform.position = _pos;
        }
        else
        {
            Debug.Log("�{�[�����ł��ꂽ��ԂȂ̂ŋO�Րݒ�ł��܂���");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�o�b�g�Ȃ�
        if (!isHit && collision.gameObject.tag == batTag)
        {
            isHit = true;

            //Trajectory��؂�
            //canControlTrajectory = false;

            //���������ɕύX
            rb.isKinematic = false;

            //�o�b�g�̑��x����{�[�����΂�
            var calcVelocity = collision.gameObject.GetComponent<CalcVelocity>();
            if (calcVelocity)
            {
                rb.velocity = calcVelocity.GetVelocity() * velocityMultiplier;
            }
            else
            {
                Debug.LogError("Bat��CalcVelocity�R���|�[�l���g���ݒ肳��Ă��܂���");
            }
        }
    }
}
