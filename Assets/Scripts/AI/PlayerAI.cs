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
    AIBehaviour m_behaviourState = AIBehaviour.DEFEND;
    GameObject m_nearestZom = null;
    [SerializeField] float m_safeDistance = 1.0f;
    bool m_behaviourLocked = false;
    Rect m_boundary;

    private void Start()
    {
        setAIBehaviour(AIBehaviour.DEFEND);
    }

    public void lockBehaviour()
    {
        m_behaviourLocked = true;
    }
    public void unlockBehaviour()
    {
        m_behaviourLocked = false;
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
                direction = stayInsideZoneBoundary(direction);
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
            direction = stayInsideZoneBoundary(direction);
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
        if (m_nearestZom)
        {
            (m_entity as Player).aim(m_nearestZom);

            Vector2 origin = transform.position;
            Vector2 direction = m_nearestZom.transform.position - transform.position;
            RaycastHit2D rayOut = Physics2D.Raycast(origin, direction);
            if (rayOut.collider)
            {
                if (rayOut.collider.gameObject.layer != 3)
                    (m_entity as Player).shoot();
            }
        }
    }

    Vector2 stayInsideZoneBoundary(Vector3 directionAlready)
    {
        getRoomBoundary();
        Vector2 directionBack = directionAlready;
        if (transform.position.x < m_boundary.xMin)
        {
            directionBack.x = 1;
        }
        else if (transform.position.x > m_boundary.xMax)
        {
            directionBack.x = -1;
        }

        if (transform.position.y < m_boundary.yMin)
        {
            directionBack.y = 1;
        }
        else if (transform.position.y > m_boundary.yMax)
        {
            directionBack.y = -1;
        }
        return directionBack.normalized;
    }

    void moveTowardsTarget()
    {
        Vector3 target = m_pathFinding.getTargetPos(out bool n);
        if (Vector3.Distance(transform.position, target) > GameVariables.ToleranceDist)
        {
            if (target != Vector3.zero)
            {
                Vector3 direction = target - transform.position;
                direction.Normalize();
                move(direction);
            }
        }
        else
        {
            if (m_pathFinding.advancePath())
                setAIBehaviour(m_agressiveBehaviour);
        }
    }

    public void setAIBehaviour(AIBehaviour _behaviour, Node _moveTarget = null)
    {
        if (m_behaviourLocked) return;
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
                if (_moveTarget)
                {
                    setTarget(_moveTarget);
                    (m_entity as Player).setMoveType(MoveType.Walking);
                }
                break;
            case AIBehaviour.RUN:
                if (_moveTarget)
                {
                    setTarget(_moveTarget);
                    (m_entity as Player).setMoveType(MoveType.Running);
                }
                break;
        }
        m_behaviourState = _behaviour;
    }

    void getRoomBoundary()
    {
        DestinationNode room = m_pathFinding.currentNode as DestinationNode;
        if (room)
        {
            m_boundary = room.getRoomBounds;
        }
    }
}
