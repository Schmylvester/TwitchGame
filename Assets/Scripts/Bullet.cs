using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_bulletSpeed;
    [SerializeField] float m_lifeTime = 10;

    bool m_explosiveBullet = false;
    [SerializeField] GameObject m_explosion;
    public bool bulletExplodes { set { m_explosiveBullet = value; } }

    private void Update()
    {
        m_lifeTime -= Time.deltaTime;
        if (m_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.right * Time.deltaTime * m_bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Zombie>().takeDamage(5, direction * 0.02f);
        }
        if (m_explosiveBullet)
        {
            Instantiate(m_explosion, transform.position, new Quaternion());
        }
        Destroy(gameObject);
    }
}
