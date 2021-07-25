using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : EntityAI
{
    [SerializeField] float m_obsessiveness = 0.0f;
    GameObject m_targetPlayer = null;

    private void Start()
    {
        getTarget();
    }

    protected override void doAThink()
    {
        moveTowardsTargetNode();
    }

    void moveTowardsTargetNode()
    {
        bool atDestination = false;
        Vector3 target = m_pathFinding.getTargetPos(out atDestination);
        if (atDestination)
        {
            findPriorityTarget();
        }
        else if (target != Vector3.zero)
        {
            if (moveTowards(target) <= GameVariables.ToleranceDist)
            {
                m_pathFinding.advancePath();
            }
        }
    }

    void getTarget()
    {
        m_targetPlayer = findNearest(SpawnManager.instance.getPlayers());
        if (m_targetPlayer)
        {
            (m_entity as Zombie).setTargetPlayer(m_targetPlayer);
            setTarget(findNearestNodeToPosition(m_targetPlayer.transform.position));
        }
    }

    Node findNearestNodeToPosition(Vector3 position)
    {
        DestinationNode[] allNodes = PlayerAIManager.instance.m_nodes;
        float dist = float.MaxValue;
        DestinationNode nearestNode = null;
        foreach (DestinationNode node in allNodes)
        {
            float myDistance = Vector3.Distance(node.transform.position, position);
            if (myDistance < dist)
            {
                dist = myDistance;
                nearestNode = node;
            }
        }

        return nearestNode;
    }

    void findPriorityTarget()
    {
        Vector3 target = m_targetPlayer.transform.position;
        moveTowards(target);
    }

    float moveTowards(Vector3 pos)
    {
        float distance = Vector3.Distance(transform.position, pos);
        if (distance > GameVariables.ToleranceDist)
        {
            if (pos != Vector3.zero)
            {
                Vector3 direction = pos - transform.position;
                direction.Normalize();
                move(direction);
            }
        }
        return distance;
    }
}
