using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    [SerializeField] protected Entity m_entity = null;
    protected abstract void doAThink();
    protected Vector3 m_moveTarget = Vector3.zero;

    private void Update()
    {
        if (GameStateManager.instance.getState() == GameState.InGame
            || GameStateManager.instance.getState() == GameState.CountingDown)
            doAThink();
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

    public void setTarget(Vector3 _target)
    {
        m_moveTarget = _target;
    }

}
