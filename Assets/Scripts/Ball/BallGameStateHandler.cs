using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameStateHandler : GameStateHandler
{
    [SerializeField] private SleepCalculate sleepCalculate;

    protected override void ChangeGameSate_EventOnChangeGameSate(GameState value)
    {
        switch (value)
        {
            case GameState.Game:
                sleepCalculate.SelfEnable();
                break;
            case GameState.Pause:
                sleepCalculate.SelfDisable();
                break;
        }
    }
}
