using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    List<GameObject> m_players = new List<GameObject>();
    List<GameObject> m_zombies = new List<GameObject>();

    public void addPlayer(GameObject _player) { m_players.Add(_player); }

    public void removePlayer(GameObject _player) { m_players.Remove(_player); }

    public void removePlayer(int _index) { m_players.RemoveAt(_index); }

    public void addZombie(GameObject _player) { m_zombies.Add(_player); }

    public void removeZombie(GameObject _player) { m_zombies.Remove(_player); }

    public void removeZombie(int _index) { m_zombies.RemoveAt(_index); }

    public List<GameObject> getPlayers() { return m_players; }
    public List<GameObject> getZombies() { return m_zombies; }
}
