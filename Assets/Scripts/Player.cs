using UnityEngine;

public class Player : Entity
{
    [SerializeField] GameObject m_bulletPrefab = null;
    GameObject m_target = null;
    [SerializeField] float m_fireRate = 0;
    float m_cooldown = 0.0f;

    private void Update()
    {
        if (m_cooldown <= 0)
        {
            if (m_target != null)
            {
                shoot();
                m_cooldown = m_fireRate;
            }
        }
        else
        {
            m_cooldown -= Time.deltaTime;
        }
    }

    public void aim(GameObject _target)
    {
        m_target = _target;
    }

    void shoot()
    {
        Vector3 directionToZom = (m_target.transform.position - transform.position).normalized;
        Transform bullet = Instantiate(m_bulletPrefab, transform.position, new Quaternion()).transform;
        bullet.right = directionToZom;
    }
}
