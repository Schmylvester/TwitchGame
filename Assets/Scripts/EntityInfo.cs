using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour
{
    [SerializeField] Transform m_healthBar = null;
    Transform m_playerToTrack = null;

    public void setTrackPlayer(Transform _player)
    {
        m_playerToTrack = _player;
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(m_playerToTrack.position);
    }

    public void setHealth(float _health)
    {
        m_healthBar.transform.localScale = new Vector3(_health, 1, 1);
    }
}
