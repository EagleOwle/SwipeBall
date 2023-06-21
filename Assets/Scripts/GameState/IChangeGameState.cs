using System;

public interface IChangeGameState
{
    event Action<GameState> EventOnChangeGameState;
}
