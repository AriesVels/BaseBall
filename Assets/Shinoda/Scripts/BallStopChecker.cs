using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStopChecker : MonoBehaviour
{
    [SerializeField] float stopSpeed = 1f;
    BattingResultController battingResultController;
    BallController ballController;
    Rigidbody myRb;
    bool sendAble = true;

    // Start is called before the first frame update
    void Start()
    {
        battingResultController = GameObject.Find("GameControllerCanvas").GetComponent<BattingResultController>();
        ballController = this.gameObject.GetComponent<BallController>();
        myRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballController.IsHit() && myRb.velocity.magnitude <= stopSpeed && sendAble)
        {
            sendAble = false;
            battingResultController.SendResult();
            Destroy(this.gameObject);
        }
    }
}
