using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : EntityAI
{
    [SerializeField] protected Player m_entity = null;

    protected override void doAThink()
    {
        if (Random.value < 0.001f)
        {
            m_entity.setTarget(new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-2.0f, 2.0f)));
        }

        GameObject nearestZom = findNearest(SpawnManager.instance.getZombies());
        if (nearestZom)
            m_entity.aim(nearestZom);
    }
}
