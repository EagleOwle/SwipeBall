using System.Collections;
using UnityEngine;

public abstract class GameStateHandler : MonoBehaviour
{
    private IChangeGameState changeGameSate;

    public void Initialise(IChangeGameState changeGameSate)
    {
        this.changeGameSate = changeGameSate;
        changeGameSate.EventOnChangeGameState += ChangeGameSate_EventOnChangeGameSate;
    }

    protected abstract void ChangeGameSate_EventOnChangeGameSate(GameState value);

    private void OnDestroy()
    {
        if (changeGameSate == null) return;
        changeGameSate.EventOnChangeGameState -= ChangeGameSate_EventOnChangeGameSate;
    }

}