using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] Animator m_animator = null;
    [SerializeField] float m_movementSpeed;
    Vector3 m_moveTarget = Vector3.zero;

    EntityInfo m_info = null;
    [SerializeField] float m_maxHealth;
    float m_health;
    protected float m_moveCooldown = 0.0f;

    private void Start()
    {
        m_health = m_maxHealth;
    }

    public void setUIInfo(EntityInfo _info)
    {
        m_info = _info;
    }

    void move(Vector3 _direction)
    {
        m_animator.SetInteger("animState", (int)AnimStates.Walk);
        transform.localScale = _direction.x < 0 ? new Vector3(-1, 1, 1) : Vector3.one;
        transform.position += (_direction * m_movementSpeed * Time.deltaTime);
    }

    public void setTarget(Vector3 _target)
    {
        m_moveTarget = _target;
    }

    private void Update()
    {
        update();
    }

    protected virtual void update()
    {
        if (m_moveCooldown <= 0)
        {
            if (Vector3.Distance(transform.position, m_moveTarget) > 0.1f && m_moveTarget != Vector3.zero)
            {
                Vector3 direction = m_moveTarget - transform.position;
                direction.Normalize();
                move(direction);
            }
        }
        {
            m_moveCooldown -= Time.deltaTime;
        }
    }

    public void takeDamage(float _damage, Vector3 _knockback)
    {
        m_moveCooldown = 0.5f;
        transform.position += _knockback;
        m_health -= _damage;
        m_info.setHealth(m_health / m_maxHealth);
        m_animator.SetInteger("animState", (int)AnimStates.Damage);
        if (m_health <= 0)
        {
            StartCoroutine(onDeath());
        }
    }

    public IEnumerator onDeath()
    {
        m_moveCooldown = 5;
        m_animator.SetInteger("animState", (int)AnimStates.Defeat);
        yield return new WaitForSeconds(2f);
        SpawnManager.instance.removePlayer(gameObject);
        SpawnManager.instance.removeZombie(gameObject);
        Destroy(m_info.gameObject);
        Destroy(gameObject);
    }
}
