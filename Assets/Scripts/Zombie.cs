using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    Player m_targetPlayer = null;
    [SerializeField] float m_attackCooldown = 0.0f;
    float m_cooldown = 0.0f;

    void Update()
    {
        if (m_cooldown <= 0 && m_targetPlayer != null)
        {
            if (Vector3.Distance(m_targetPlayer.transform.position, transform.position) < 0.1f)
            {
                m_targetPlayer.takeDamage(5, Vector3.zero);
                m_cooldown = m_attackCooldown;
            }
        }
        else
        {
            m_cooldown -= Time.deltaTime;
        }
    }

    public override void move(Vector3 _direction)
    {
        if (m_cooldown > 0)
            return;
        base.move(_direction);
    }

    public void setTargetPlayer(GameObject _player)
    {
        m_targetPlayer = _player.GetComponent<Player>();
    }
}
