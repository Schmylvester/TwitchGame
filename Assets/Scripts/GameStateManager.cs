using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    GameState m_gameState = GameState.Lobby;

    public void startGame()
    {
        m_gameState = GameState.InGame;
    }

    public void endGame()
    {
        m_gameState = GameState.Lobby;
    }

    public GameState getState()
    {
        return m_gameState;
    }
}
