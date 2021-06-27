using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : EntitySpawner
{
    [SerializeField] float m_maxSpawnRate = 1.0f;
    [SerializeField] float m_minSpawnRate = 1.0f;
    [SerializeField] float m_spawnRateInterval = 1.0f;
    [SerializeField] float m_spawnRateAmount = 0.01f;
    float m_increaseRateTimer = 0.0f;
    float m_spawnRate = 1.0f;
    [SerializeField] protected Transform m_spawnPosParent = null;

    private void Start()
    {
        m_spawnRate = m_maxSpawnRate;
        StartCoroutine(spawnZombies());
    }

    private void Update()
    {
        if (m_spawnRate > m_minSpawnRate)
        {
            m_increaseRateTimer -= Time.deltaTime;
            if (m_increaseRateTimer <= 0)
            {
                m_spawnRate = Mathf.Max(m_spawnRate - m_spawnRateAmount, m_minSpawnRate);
                m_increaseRateTimer = m_spawnRateInterval;
            }
        }
    }

    IEnumerator spawnZombies()
    {
        yield return new WaitForSeconds(m_spawnRate);
        if (m_gameStateManager.getState() == GameState.InGame)
        {
            GameObject spawnedZombie = Instantiate(m_entityPrefab);
            Transform position = m_spawnPosParent.GetChild(Random.Range(0, m_spawnPosParent.childCount));
            spawnedZombie.transform.position = position.position;
            spawnedZombie.name = "Felix" + Random.Range(0, 150);
            setModel(spawnedZombie, Random.Range(0, m_models.Length));
            addInfoPanel(spawnedZombie);
            SpawnManager.instance.addZombie(spawnedZombie);
        }
        yield return spawnZombies();    
    }

    void addInfoPanel(GameObject _zombie)
    {
        EntityInfo zombieInfo = Instantiate(m_infoPanelPrefab, m_gameCanvas).GetComponent<EntityInfo>();
        zombieInfo.setTrackPlayer(_zombie.transform);
        _zombie.GetComponent<Entity>().setUIInfo(zombieInfo);
    }
}
