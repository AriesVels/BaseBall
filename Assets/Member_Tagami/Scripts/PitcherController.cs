using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PitcherController : MonoBehaviour
{
    [Header("ストライクになった場合呼ぶ関数を設定してください")]
    [SerializeField] UnityEvent strikeEvent;

    //軌跡の設定
    [Header("軌跡を設定してください")]
    [SerializeField] List<Trajectory> trajectorys = new List<Trajectory>();
    int indexInOrder;
    //random
    [SerializeField] bool randomIndex;
    [SerializeField] bool autoPitch;

    [Header("Ball")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform commonStartPoint;

    [Header("Animation")]
    [SerializeField] Animator pitcherAnimator;
    [SerializeField] float throwAnimWaitSeconds = 1.5f;
    Quaternion initialPitcherModelRotation;
    Vector3 initialPitcherModelPosition;

    GameObject ballObject;
    Trajectory runningTrajectory;
    bool oldStrike;

    // Start is called before the first frame update
    void Start()
    {
        if (trajectorys.Count <= 0)
        {
            Debug.LogError("必ず一つは軌跡(Trajectory)を設定して下さい");
        }

        initialPitcherModelPosition = pitcherAnimator.transform.position;
        initialPitcherModelRotation = pitcherAnimator.transform.rotation;

        StartCoroutine(CoPitchLoop());


    }

    private void Update()
    {
        if (runningTrajectory)
        {
            if (runningTrajectory.strike && !oldStrike)
            {
                Debug.Log("ストライク！");
                strikeEvent.Invoke();
            }
            oldStrike = runningTrajectory.strike;
        }
        else
        {
            oldStrike = false;
        }
    }

    IEnumerator CoPitchLoop()
    {
        while (true)
        {
            if (autoPitch)
            {
                Throw();
            }
            yield return new WaitForSeconds(5);
        }
    }

    [ContextMenu("Throw")]
    public void Throw()
    {
        StartCoroutine(CoThrow());
    }

    IEnumerator CoThrow()
    {
        //ボールオブジェクトが存在する場合はじく
        if (ballObject)
        {
            Debug.LogError("まだ前回投げたボールが存在しているため新しく投げることができません");
            yield break;
        }

        //位置補正
        pitcherAnimator.transform.position = initialPitcherModelPosition;
        pitcherAnimator.transform.rotation = initialPitcherModelRotation;
        pitcherAnimator.SetTrigger("ThrowTrigger");

        //アニメーション終了待機
        yield return new WaitForSeconds(throwAnimWaitSeconds);


        //右手の子供にボールの作成
        ballObject = Instantiate(ballPrefab, commonStartPoint.position, commonStartPoint.rotation);

        Trajectory trajectory;
        if (randomIndex)
        {
            trajectory = trajectorys[Random.Range(0, trajectorys.Count)];
        }
        else
        {
            trajectory = trajectorys[indexInOrder];
            indexInOrder++;
            if (indexInOrder >= trajectorys.Count)
            {
                indexInOrder = 0;
            }

        }

        runningTrajectory = trajectory;

        //軌跡に投げたボールを教えてあげる
        trajectory.ThrowTrajectory(ballObject.GetComponent<BallController>());
    }

    public bool CanThrow()
    {
        return !ballObject;
    }
}
