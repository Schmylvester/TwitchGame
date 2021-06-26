using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_bulletSpeed;
    [SerializeField] float m_lifeTime = 10;

    private void Update()
    {
        m_lifeTime -= Time.deltaTime;
        if (m_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.right * Time.deltaTime * m_bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Zombie>().takeDamage(5, direction * 0.2f);
        }
        Destroy(gameObject);
    }
}
