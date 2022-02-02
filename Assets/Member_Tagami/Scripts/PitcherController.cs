using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PitcherController : MonoBehaviour
{
    [Header("�X�g���C�N�ɂȂ����ꍇ�ĂԊ֐���ݒ肵�Ă�������")]
    [SerializeField] UnityEvent strikeEvent;

    //�O�Ղ̐ݒ�
    [Header("�O�Ղ�ݒ肵�Ă�������")]
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
            Debug.LogError("�K����͋O��(Trajectory)��ݒ肵�ĉ�����");
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
                Debug.Log("�X�g���C�N�I");
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
        //�{�[���I�u�W�F�N�g�����݂���ꍇ�͂���
        if (ballObject)
        {
            Debug.LogError("�܂��O�񓊂����{�[�������݂��Ă��邽�ߐV���������邱�Ƃ��ł��܂���");
            yield break;
        }

        //�ʒu�␳
        pitcherAnimator.transform.position = initialPitcherModelPosition;
        pitcherAnimator.transform.rotation = initialPitcherModelRotation;
        pitcherAnimator.SetTrigger("ThrowTrigger");

        //�A�j���[�V�����I���ҋ@
        yield return new WaitForSeconds(throwAnimWaitSeconds);


        //�E��̎q���Ƀ{�[���̍쐬
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

        //�O�Ղɓ������{�[���������Ă�����
        trajectory.ThrowTrajectory(ballObject.GetComponent<BallController>());
    }

    public bool CanThrow()
    {
        return !ballObject;
    }
}
