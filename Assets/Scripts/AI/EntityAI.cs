using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    protected abstract void doAThink();

    private void Update()
    {
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
}
