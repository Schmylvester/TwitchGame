using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIBehaviour
{
    Null = -1,
    // stay still and kill zombies
    HOLD,
    // stay still and kill zombies
    DEFEND,
    // track down nearest zombie and kill
    ATTACK,
    // move to target position, shooting zombies on the way
    WALK,
    // move quickly to target position, not shooting zombies
    RUN
}

public class PlayerAI : EntityAI
{
    AIBehaviour m_agressiveBehaviour = AIBehaviour.DEFEND;
    AIBehaviour m_behaviourState = AIBehaviour.HOLD;
    GameObject m_nearestZom = null;
    [SerializeField] float m_safeDistance = 1.0f;
    [SerializeField] Vector2 m_minWorldBoundary;
    [SerializeField] Vector2 m_maxWorldBoundary;

    private void Start()
    {
        setAIBehaviour(AIBehaviour.DEFEND);
    }

    protected override void doAThink()
    {
        switch (m_behaviourState)
        {
            case AIBehaviour.HOLD: hold(); break;
            case AIBehaviour.DEFEND: defend(); break;
            case AIBehaviour.ATTACK: attack(); break;
            case AIBehaviour.WALK: walk(); break;
            case AIBehaviour.RUN: run(); break;
            default: break;
        }

        m_nearestZom = findNearest(SpawnManager.instance.getZombies());
    }

    void hold()
    {
        shootNearestZom();
    }

    void defend()
    {
        (m_entity as Player).setMoveType(MoveType.Standing);
        if (m_nearestZom)
        {
            shootNearestZom();
            if (Vector3.Distance(transform.position, m_nearestZom.transform.position) < m_safeDistance)
            {
                (m_entity as Player).setMoveType(MoveType.Walking);
                Vector3 direction = (transform.position - m_nearestZom.transform.position).normalized;
                direction = stayInsideWorldBoundary(direction);
                move(direction);
            }
        }
    }

    void attack()
    {
        if (m_nearestZom)
        {
            shootNearestZom();
            Vector3 direction = (m_nearestZom.transform.position - transform.position).normalized;
            if (Vector3.Distance(transform.position, m_nearestZom.transform.position) < m_safeDistance)
            {
                direction *= -1;
            }
            direction = stayInsideWorldBoundary(direction);
            move(direction);
        }
    }

    void walk()
    {
        shootNearestZom();
        moveTowardsTarget();
    }

    void run()
    {
        moveTowardsTarget();
    }

    void shootNearestZom()
    {
        (m_entity as Player).aim(m_nearestZom);
        (m_entity as Player).shoot();
    }

    Vector2 stayInsideWorldBoundary(Vector3 directionAlready)
    {
        Vector2 directionBack = directionAlready;
        if (transform.position.x < m_minWorldBoundary.x)
        {
            directionBack.x = 1;
        }
        else if (transform.position.x > m_maxWorldBoundary.x)
        {
            directionBack.x = -1;
        }

        if (transform.position.y < m_minWorldBoundary.y)
        {
            directionBack.y = 1;
        }
        else if (transform.position.y > m_maxWorldBoundary.y)
        {
            directionBack.y = -1;
        }
        return directionBack.normalized;
    }

    void moveTowardsTarget()
    {
        if (Vector3.Distance(transform.position, m_moveTarget) > 0.01f)
        {
            if (m_moveTarget != Vector3.zero)
            {
                Vector3 direction = m_moveTarget - transform.position;
                direction.Normalize();
                move(direction);
            }
        }
        else
        {
            setAIBehaviour(m_agressiveBehaviour);
        }
    }

    public void setAIBehaviour(AIBehaviour _behaviour, Vector3 _moveTarget = new Vector3())
    {
        switch (_behaviour)
        {
            case AIBehaviour.HOLD:
                (m_entity as Player).setMoveType(MoveType.Standing);
                m_agressiveBehaviour = AIBehaviour.HOLD;
                break;
            case AIBehaviour.DEFEND:
                (m_entity as Player).setMoveType(MoveType.Standing);
                m_agressiveBehaviour = AIBehaviour.DEFEND;
                break;
            case AIBehaviour.ATTACK:
                (m_entity as Player).setMoveType(MoveType.Walking);
                m_agressiveBehaviour = AIBehaviour.ATTACK;
                break;
            case AIBehaviour.WALK:
                setTarget(_moveTarget);
                (m_entity as Player).setMoveType(MoveType.Walking);
                break;
            case AIBehaviour.RUN:
                setTarget(_moveTarget);
                (m_entity as Player).setMoveType(MoveType.Running);
                break;
        }
        m_behaviourState = _behaviour;
    }
}
