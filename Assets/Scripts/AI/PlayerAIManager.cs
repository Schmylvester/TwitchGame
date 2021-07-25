using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIManager : Singleton<PlayerAIManager>
{
    [SerializeField] TwitchReader m_twitchReader;
    public DestinationNode[] m_nodes = null;

    void Start()
    {
        m_twitchReader.m_chatEvent += parseAIBehaviour;
    }

    void parseAIBehaviour(string user, string[] commands)
    {
        if (commands.Length < 1)
        {
            return;
        }
        string command = commands[0].TrimStart('!');
        if (command.ToUpper() == "POWER")
        {
            GameObject userObject = SpawnManager.instance.getPlayers().Find(
                (GameObject obj) => obj.name == user);
            if (userObject)
            {
                userObject.GetComponent<Player>().usePower();
            }
        }
        else
        {
            string argToParse = command.ToUpper() == "STANCE" ? commands[1] : command;
            AIBehaviour aIBehaviour = AIBehaviour.Null;
            if (Enum.TryParse(argToParse, out aIBehaviour))
            {
                GameObject userObject = SpawnManager.instance.getPlayers().Find(
                    (GameObject obj) => obj.name == user);
                if (userObject)
                {
                    Node moveTarg = null;
                    if (aIBehaviour == AIBehaviour.WALK || aIBehaviour == AIBehaviour.RUN && commands.Length > 1)
                        moveTarg = getMoveTarget(commands[1]);
                    userObject.GetComponent<PlayerAI>().setAIBehaviour(aIBehaviour, moveTarg);
                }
            }
        }
    }

    Node getMoveTarget(string arg)
    {
        char n = arg[0];
        int i = (int)n - 65;
        if (i >= 0 && i < m_nodes.Length)
            return m_nodes[i];
        return null;
    }
}
