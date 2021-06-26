using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCountdown : MonoBehaviour
{
    [SerializeField] GameStateManager m_gameStateManager = null;
    [SerializeField] Button m_skipButton = null;
    [SerializeField] Button m_startButton = null;
    [SerializeField] Text m_countdownText = null;
    [SerializeField] float m_countdownTime = 0.0f;
    [SerializeField] Canvas m_lobbyCanvas = null;
    float m_countdownRemaining = 0.0f;

    public void startCountdown()
    {
        m_countdownText.gameObject.SetActive(true);
        m_skipButton.gameObject.SetActive(true);
        m_startButton.gameObject.SetActive(false);
        m_countdownRemaining = m_countdownTime;
    }

    public void endCountdown()
    {
        m_gameStateManager.startGame();
        m_lobbyCanvas.gameObject.SetActive(false);
        m_countdownRemaining = 0;
    }

    private void Update()
    {
        if (m_countdownText.isActiveAndEnabled)
        {
            if (m_countdownRemaining > 0)
            {
                m_countdownRemaining -= Time.deltaTime;
            }
            else
            {
                endCountdown();
            }
            m_countdownText.text = m_countdownRemaining.ToString("f2");
        }
    }
}