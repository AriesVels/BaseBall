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
            Debug.Log("ボールが打たれた状態なので軌跡設定できません");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //バットなら
        if (!isHit && collision.gameObject.tag == batTag)
        {
            isHit = true;

            //Trajectoryを切る
            //canControlTrajectory = false;

            //物理挙動に変更
            rb.isKinematic = false;

            //バットの速度からボールを飛ばす
            var calcVelocity = collision.gameObject.GetComponent<CalcVelocity>();
            if (calcVelocity)
            {
                rb.velocity = calcVelocity.GetVelocity() * velocityMultiplier;
            }
            else
            {
                Debug.LogError("BatにCalcVelocityコンポーネントが設定されていません");
            }
        }
    }
}
