using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : EntitySpawner
{
    [SerializeField] TwitchReader m_twitchReader = null;
    List<string> m_joinedPlayers = new List<string>();
    [SerializeField] DestinationNode m_spawnRoom = null;

    private void Start()
    {
        m_twitchReader.m_chatEvent += checkSpawn;
    }

    void checkSpawn(string user, string[] messageParts)
    {
        if (messageParts[0] == "!JOIN" && !m_joinedPlayers.Contains(user))
        {
            string character = messageParts.Length > 1 ? messageParts[1] : "";
            spawnPlayer(user, character, m_twitchReader.getUsernameColour(user));
        }
    }

    public void debugSpawn()
    {
        spawnPlayer("Martin", "", new Color(Random.value, Random.value, Random.value));
    }

    void spawnPlayer(string user, string character, Color _nameColour)
    {
        int characterIndex = -1;
        switch (character)
        {
            case "PRIYAH":
                characterIndex = 0;
                break;
            case "XAVIER":
                characterIndex = 1;
                break;
            case "RICH":
                characterIndex = 2;
                break;
            default:
                characterIndex = Random.Range(0, 3);
                break;
        }

        Player spawnedPlayer = Instantiate(m_entityPrefab).GetComponent<Player>();
        UnitClass[] classes = new UnitClass[]
        {
            new Priyah(),
            new Xavier(),
            new Rich()
        };
        spawnedPlayer.setClass(classes[characterIndex]);

        m_joinedPlayers.Add(user);
        spawnedPlayer.name = user;
        spawnedPlayer.transform.position = m_spawnRoom.getPointInRoom();
        setModel(spawnedPlayer.gameObject, characterIndex);
        addInfoPanel(spawnedPlayer.gameObject, user, _nameColour);
        SpawnManager.instance.addPlayer(spawnedPlayer.gameObject);
    }

    void addInfoPanel(GameObject _player, string _name, Color _nameColour)
    {
        PlayerInfo playerInfo = Instantiate(m_infoPanelPrefab, m_gameCanvas).GetComponent<PlayerInfo>();
        playerInfo.setTrackPlayer(_player.transform, _name, _nameColour);
        _player.GetComponent<Entity>().setUIInfo(playerInfo);
    }
}
