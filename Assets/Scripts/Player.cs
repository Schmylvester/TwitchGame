using System.Collections;
using UnityEngine;

public enum MoveType
{
    Null = -1,
    Standing,
    Walking,
    Running
}

public class Player : Entity
{
    [SerializeField] GameObject m_bulletPrefab = null;
    GameObject m_target = null;
    float m_cooldown = 0.0f;
    bool m_isMoving = false;
    float m_dodgeChance = 0.0f;
    UnitClass m_class;

    public void heal(float _healAmount)
    {
        m_health = Mathf.Min(m_health + (m_maxHealth * _healAmount), m_maxHealth);
    }

    public void setClass(UnitClass _class)
    {
        m_class = _class;
    }

    public void aim(GameObject _target)
    {
        m_target = _target;
    }

    public void setMoveType(MoveType _moveType)
    {
        switch (_moveType)
        {
            case MoveType.Standing:
                m_isMoving = false;
                m_dodgeChance = m_class.getStaticDodge();
                break;
            case MoveType.Walking:
                m_isMoving = true;
                m_movementSpeed = m_class.getWalkSpeed();
                m_dodgeChance = m_class.getWalkingDodge();
                break;
            case MoveType.Running:
                m_isMoving = true;
                m_movementSpeed = m_class.getRunSpeed();
                m_dodgeChance = m_class.getRunningDodge();
                break;

        }
    }

    public void shoot()
    {
        if (m_cooldown <= 0)
        {
            if (m_target != null)
            {
                m_animator.SetInteger("animState", (int)AnimStates.Attack);
                Vector3 directionToZom = (m_target.transform.position - transform.position).normalized;
                transform.localScale = (directionToZom.x < 0 ? new Vector3(-1, 1, 1) : Vector3.one) * m_scale;
                float angleToZom = Vector3.SignedAngle(Vector3.right, directionToZom, Vector3.forward);
                Transform bullet = Instantiate(m_bulletPrefab, transform.position, new Quaternion()).transform;
                float accuracy = m_isMoving ? m_class.getMovingAccuracy() : m_class.getStaticAccuracy();
                float angleToShoot = angleToZom + ((1 - accuracy) * Mathf.Lerp(-90, 90, Random.value));
                bullet.Rotate(new Vector3(0, 0, angleToShoot));
                m_cooldown = m_class.getFireRate();
            }
        }
        else
        {
            m_cooldown -= Time.deltaTime;
        }
    }

    public override void takeDamage(float _damage, Vector3 _knockback)
    {
        if (Random.value > m_dodgeChance)
        {
            base.takeDamage(_damage, _knockback);
        }
    }

    public override IEnumerator onDeath()
    {
        SpawnManager.instance.removePlayer(gameObject);
        yield return base.onDeath();
        GameStateManager.instance.checkGameOver();
    }
}
