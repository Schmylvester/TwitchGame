using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    [SerializeField] protected Entity m_entity = null;
    protected abstract void doAThink();
    protected PathFinding m_pathFinding = new PathFinding();

    private void Update()
    {
        if (GameStateManager.instance.getState() == GameState.InGame
            || GameStateManager.instance.getState() == GameState.CountingDown)
            doAThink();
    }

    public void setPathFindingStartPos(Node _node)
    {
        m_pathFinding.spawn(_node);
    }

    protected GameObject findNearest(List<GameObject> gameObjects)
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestObject = null;

        foreach (GameObject obj in gameObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj;
            }
        }

        return nearestObject;
    }

    public void move(Vector3 _direction)
    {
        m_entity.move(_direction);
    }

    public void setTarget(Node _target)
    {
        m_pathFinding.createPath(_target);
    }
}
