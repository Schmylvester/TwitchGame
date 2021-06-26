using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : EntitySpawner
{
    [SerializeField] TwitchReader m_twitchReader = null;
    List<string> m_joinedPlayers = new List<string>();

    private void Start()
    {
        m_twitchReader.m_chatEvent += checkSpawn;
    }

    void checkSpawn(string user, string message)
    {
        string[] messageParts = message.Split(' ');
        if (messageParts[0] == "!join" && !m_joinedPlayers.Contains(user)
            && m_gameStateManager.getState() == GameState.Lobby)
        {
            spawnPlayer(user, messageParts.Length > 1 ? messageParts[1] : "");
        }
    }

    public void debugSpawn()
    {
        spawnPlayer("Martin", "");
    }

    void spawnPlayer(string user, string character)
    {
        int characterIndex = -1;
        switch (character.ToLower())
        {
            case "priyah":
                characterIndex = 0;
                break;
            case "xavier":
                characterIndex = 1;
                break;
            case "rich":
                characterIndex = 2;
                break;
            default:
                characterIndex = Random.Range(0, 3);
                break;
        }

        m_joinedPlayers.Add(user);
        GameObject spawnedPlayer = Instantiate(m_entityPrefab);
        spawnedPlayer.name = user;
        spawnedPlayer.transform.position = Vector3.Lerp(Vector3.one * -1, Vector3.one, Random.value);
        setModel(spawnedPlayer, characterIndex);
        addInfoPanel(spawnedPlayer, user);
        SpawnManager.instance.addPlayer(spawnedPlayer);
    }

    void addInfoPanel(GameObject _player, string _name)
    {
        PlayerInfo playerInfo = Instantiate(m_infoPanelPrefab, m_gameCanvas).GetComponent<PlayerInfo>();
        playerInfo.setTrackPlayer(_player.transform, _name);
        _player.GetComponent<Entity>().setUIInfo(playerInfo);
    }
}
