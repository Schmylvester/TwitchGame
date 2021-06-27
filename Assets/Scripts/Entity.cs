using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Animator m_animator = null;
    [SerializeField] protected float m_movementSpeed;

    EntityInfo m_info = null;
    [SerializeField] protected float m_maxHealth;
    protected float m_health;
    protected float m_moveCooldown = 0.0f;
    [SerializeField] protected float m_scale = 1.0f;

    private void Start()
    {
        m_health = m_maxHealth;
    }

    public void setUIInfo(EntityInfo _info)
    {
        m_info = _info;
    }

    public virtual void move(Vector3 _direction)
    {
        if (m_moveCooldown <= 0)
        {
            _direction.z = 0;
            m_animator.SetInteger("animState", (int)AnimStates.Walk);
            transform.localScale = (_direction.x < 0 ? new Vector3(-1, 1, 1) : Vector3.one) * m_scale;
            transform.position += (_direction * m_movementSpeed * Time.deltaTime);
        }
        else
        {
            m_moveCooldown -= Time.deltaTime;
        }
    }

    public virtual void takeDamage(float _damage, Vector3 _knockback)
    {
        m_moveCooldown = 0.5f;
        transform.position += _knockback;
        m_health -= _damage;
        m_health = Mathf.Max(m_health - _damage, 0);
        m_info.setHealth(m_health / m_maxHealth);
        m_animator.SetInteger("animState", (int)AnimStates.Damage);
        if (m_health == 0)
        {
            StartCoroutine(onDeath());
        }
    }

    public virtual IEnumerator onDeath()
    {
        SpawnManager.instance.removeZombie(gameObject);
        m_moveCooldown = 5;
        m_animator.SetInteger("animState", (int)AnimStates.Defeat);
        yield return new WaitForSeconds(2f);
        Destroy(m_info.gameObject);
        Destroy(gameObject);
    }
}
