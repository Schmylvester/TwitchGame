using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : EntityAI
{
    [SerializeField] Zombie m_entity = null;

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
    }

    void getTarget()
    {
        GameObject nearestPlayer = findNearest(SpawnManager.instance.getPlayers());
        if (nearestPlayer)
        {
            m_entity.setTargetPlayer(nearestPlayer);
            m_entity.setTarget(nearestPlayer.transform.position);
        }
    }
}
