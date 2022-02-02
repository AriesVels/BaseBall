using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewriteResult : MonoBehaviour
{
    [SerializeField] bool isWall = false;
    [SerializeField] float rewriteSpeed = 3f;
    BattingResultController battingResultController;
    [SerializeField] string result;

    // Start is called before the first frame update
    void Start()
    {
        battingResultController = GameObject.Find("GameController").GetComponent<BattingResultController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isWall && other.gameObject.CompareTag("Ball"))
        {
            if (battingResultController.RewriteResult(result)) battingResultController.SendResult();
            else Debug.Log("リザルトに送る文字列がenumと違うかも");

            // 結果送り終わったらボールを消す
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude <= rewriteSpeed)
            {
                if (battingResultController.RewriteResult(result)) Debug.Log("書き換え成功");
                else Debug.Log("書き換え失敗");
            }
        }
    }
}