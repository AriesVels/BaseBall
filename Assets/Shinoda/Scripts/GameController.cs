using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    enum RunnerState
    {
        first = 0,
        second = 1,
        third = 2,
        home = 3,
    }
    [SerializeField] List<RunnerState> runner;

    [SerializeField] int enemyScore;
    [SerializeField] int playerScore;
    [SerializeField] int outCount;
    [SerializeField] int strikeCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("SingleHit")] public void SingleHit()
    {
        for (int num = 0; num < runner.Count; num++)
        {
            var next = (int)runner[num];
            next += 1;
            if (next > 3) next = 3;
            runner[num] = (RunnerState)Enum.ToObject(typeof(RunnerState), next);
            Debug.Log(runner[num]);
        }

        RunnerState newRunner = RunnerState.first;
        runner.Add(newRunner);

        List<RunnerState> score = runner.FindAll(state => state == RunnerState.home);
        playerScore += score.Count;
        runner.RemoveAll(state => state == RunnerState.home);

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("TwoBaseHit")] public void TwoBaseHit()
    {
        for (int num = 0; num < runner.Count; num++)
        {
            var next = (int)runner[num];
            next += 2;
            if (next > 3) next = 3;
            runner[num] = (RunnerState)Enum.ToObject(typeof(RunnerState), next);
            Debug.Log(runner[num]);
        }

        RunnerState newRunner = RunnerState.second;
        runner.Add(newRunner);

        List<RunnerState> score = runner.FindAll(state => state == RunnerState.home);
        playerScore += score.Count;
        runner.RemoveAll(state => state == RunnerState.home);

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("ThreeBaseHit")] public void ThreeBaseHit()
    {
        for (int num = 0; num < runner.Count; num++)
        {
            var next = (int)runner[num];
            next += 3;
            if (next > 3) next = 3;
            runner[num] = (RunnerState)Enum.ToObject(typeof(RunnerState), next);
            Debug.Log(runner[num]);
        }

        RunnerState newRunner = RunnerState.third;
        runner.Add(newRunner);

        List<RunnerState> score = runner.FindAll(state => state == RunnerState.home);
        playerScore += score.Count;
        runner.RemoveAll(state => state == RunnerState.home);

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("HomeRun")] public void HomeRun()
    {
        for (int num = 0; num < runner.Count; num++)
        {
            var next = (int)runner[num];
            next += 4;
            if (next > 3) next = 3;
            runner[num] = (RunnerState)Enum.ToObject(typeof(RunnerState), next);
            Debug.Log(runner[num]);
        }

        RunnerState newRunner = RunnerState.home;
        runner.Add(newRunner);

        List<RunnerState> score = runner.FindAll(state => state == RunnerState.home);
        playerScore += score.Count;
        runner.RemoveAll(state => state == RunnerState.home);

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("Foul")] public void Foul()
    {
        if (strikeCount < 2)
        {
            strikeCount += 1;
        }

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("Strike")] public void Strike()
    {
        strikeCount += 1;
        if (strikeCount == 3)
        {
            strikeCount = 0;
            Out();
        }

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("Out")] public void Out()
    {
        outCount += 1;
        if (outCount >= 3)
        {
            outCount = 0;   // Debug時のみ
            Debug.Log("GameOver");
            //gameover
        }

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }

    [ContextMenu("DoublePlay")] public void DoublePlay()
    {
        if (runner.Count == 0) outCount += 1;
        else
        {
            outCount += 2;
            // 一番前の走者がアウトになる処理
            int removeRunner = 0;
            RunnerState removeState = RunnerState.first;
            for (int num = 0; num < runner.Count; num++)
            {
                if ((int)runner[num] >= (int)removeState)
                {
                    removeRunner = num;
                    removeState = runner[num];
                }
            }
            runner.RemoveAt(removeRunner);
        }
        if (outCount >= 3)
        {
            outCount = 0;   // Debug時のみ
            Debug.Log("GameOver");
            //gameover
        }

        Debug.Log("PScore" + playerScore + "\n"
            + "EScore" + enemyScore + "\n"
            + "StrikeCount" + strikeCount + "\n"
            + "OutCount" + outCount + "\n");
    }
}
