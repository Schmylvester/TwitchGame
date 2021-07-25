using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float m_time = 0.0f;
    [SerializeField] float m_explosionSize;
    [SerializeField] float m_explosionTime;

    private void Start()
    {
        transform.localScale = Vector3.zero;        
    }

    void Update()
    {
        m_time += Time.deltaTime;
        transform.localScale = Vector3.one * Mathf.Lerp(0, m_explosionSize, Mathf.Pow(m_time / m_explosionTime, 0.5f));
        if (m_time > m_explosionTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Zombie>().takeDamage(5000, direction * 0.2f);
        }
    }
}
