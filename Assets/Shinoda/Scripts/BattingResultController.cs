using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattingResultController : MonoBehaviour
{
    enum BattingResult
    {
        Single,
        TwoBase,
        ThreeBase,
        HomeRun,
        Foul,
        Strike,
        Out,
        DoublePlay,
    }
    [SerializeField] BattingResult result;
    GameController gameControllerComponent;

    // Start is called before the first frame update
    void Start()
    {
        gameControllerComponent = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool RewriteResult(string _str)
    {
        if (Enum.TryParse(_str, out BattingResult result)) return true;
        else return false;
    }

    public void SendStrike()
    {
        gameControllerComponent.Strike();
        result = BattingResult.Out;
    }

    public void SendResult()
    {
        switch (result)
        {
            case BattingResult.Single:
                gameControllerComponent.SingleHit();
                break;
            case BattingResult.TwoBase:
                gameControllerComponent.TwoBaseHit();
                break;
            case BattingResult.ThreeBase:
                gameControllerComponent.ThreeBaseHit();
                break;
            case BattingResult.HomeRun:
                gameControllerComponent.HomeRun();
                break;
            case BattingResult.Foul:
                gameControllerComponent.Foul();
                break;
            case BattingResult.Strike:  // ‚±‚ê‚ÍŒÄ‚Î‚ê‚È‚¢‚Í‚¸
                gameControllerComponent.Strike();
                break;
            case BattingResult.Out:
                gameControllerComponent.Out();
                break;
            case BattingResult.DoublePlay:
                gameControllerComponent.DoublePlay();
                break;
        }
        result = BattingResult.Out;
    }
}