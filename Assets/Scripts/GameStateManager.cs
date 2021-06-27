using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : Singleton<GameStateManager>
{
    GameState m_gameState = GameState.Lobby;
    [SerializeField] Text m_gameOverText = null;
    [SerializeField] Transform m_canvas = null;
    [SerializeField] GameObject m_zones;
    [SerializeField] GameObject m_zoneUI;
    public void startGame()
    {
        m_gameState = GameState.InGame;
        m_zones.SetActive(true);
        m_zoneUI.SetActive(true);
    }

    public void startCountdown()
    {
        m_gameState = GameState.CountingDown;
    }

    public void endGame()
    {
        m_gameState = GameState.Lobby;
    }

    public bool checkGameOver()
    {
        if (SpawnManager.instance.getPlayers().Count == 0)
        {
            gameOver();
            return true;
        }
        return false;
    }

    public GameState getState()
    {
        return m_gameState;
    }

    private void gameOver()
    {
        m_canvas.gameObject.SetActive(false);
        m_gameOverText.text = "Ha ha. Game over.";
        m_gameState = GameState.GameOver;
    }
}
