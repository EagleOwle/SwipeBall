using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGameStateHandler : GameStateHandler
{
    protected override void ChangeGameSate_EventOnChangeGameSate(GameState value)
    {
        switch (value)
        {
            case GameState.Game:
                gameObject.SetActive(true);
                break;
            case GameState.Pause:
                gameObject.SetActive(false);
                break;
        }
    }
}
