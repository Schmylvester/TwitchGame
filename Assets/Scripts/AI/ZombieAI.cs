using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : EntityAI
{
    [SerializeField] float m_obsessiveness = 0.0f;

    private void Start()
    {
        getTarget();
    }

    protected override void doAThink()
    {
        if (Random.value > m_obsessiveness)
        {
            getTarget();
        }
        m_entity.move((m_moveTarget - transform.position).normalized);
    }

    void getTarget()
    {
        GameObject nearestPlayer = findNearest(SpawnManager.instance.getPlayers());
        if (nearestPlayer)
        {
            (m_entity as Zombie).setTargetPlayer(nearestPlayer);
            setTarget(nearestPlayer.transform.position);
        }
    }
}
