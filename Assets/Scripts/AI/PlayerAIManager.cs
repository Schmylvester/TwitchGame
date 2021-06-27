using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIManager : MonoBehaviour
{
    [SerializeField] TwitchReader m_twitchReader;
    [SerializeField] Transform[] m_nodes = null;

    void Start()
    {
        m_twitchReader.m_chatEvent += parseAIBehaviour;
    }

    void parseAIBehaviour(string user, string[] commands)
    {
        if (commands.Length <= 1)
        {
            return;
        }
        string command = commands[0].TrimStart('!');
        string argToParse = command.ToUpper() == "STANCE" ? commands[1] : command;
        AIBehaviour aIBehaviour = AIBehaviour.Null;
        if (Enum.TryParse(argToParse, out aIBehaviour))
        {
            GameObject userObject = SpawnManager.instance.getPlayers().Find(
                (GameObject obj) => obj.name == user);
            if (userObject)
            {
                Vector3 moveTarg = Vector3.zero;
                if (aIBehaviour == AIBehaviour.WALK || aIBehaviour == AIBehaviour.RUN && commands.Length > 1)
                    moveTarg = getMoveTarget(commands[1]);
                userObject.GetComponent<PlayerAI>().setAIBehaviour(aIBehaviour, moveTarg);
            }
        }
    }

    Vector3 getMoveTarget(string arg)
    {
        char n = arg[0];
        int i = (int)n - 65;
        if (i >= 0 && i < m_nodes.Length)
            return m_nodes[i].position + ((Vector3)UnityEngine.Random.insideUnitCircle * 0.2f);
        return Vector3.zero;
    }
}
