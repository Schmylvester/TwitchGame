using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DebugSpawn
{
    Off = 0,
    OneAtATime,
    LotsAndLots
}

public class ZombieSpawner : EntitySpawner
{
    [SerializeField] DebugSpawn m_debugSpawnMode = DebugSpawn.Off;
    [SerializeField] float m_maxSpawnRate = 1.0f;
    [SerializeField] float m_minSpawnRate = 1.0f;
    [SerializeField] float m_spawnRateInterval = 1.0f;
    [SerializeField] float m_spawnRateAmount = 0.01f;
    float m_increaseRateTimer = 0.0f;
    float m_spawnRate = 1.0f;
    [SerializeField] protected Node[] m_spawnPositions = null;

    private void Start()
    {
        m_spawnRate = m_maxSpawnRate;
        if (m_debugSpawnMode == DebugSpawn.LotsAndLots)
        {
            m_spawnRate = 0.2f;
        }
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
        if (m_debugSpawnMode != DebugSpawn.OneAtATime || SpawnManager.instance.getZombies().Count == 0)
        {
            if (m_gameStateManager.getState() == GameState.InGame)
            {
                GameObject spawnedZombie = Instantiate(m_entityPrefab);
                Node spawnNode = m_spawnPositions[Random.Range(0, m_spawnPositions.Length)];
                spawnedZombie.GetComponent<ZombieAI>().setPathFindingStartPos(spawnNode);
                spawnedZombie.transform.position = spawnNode.transform.position;
                spawnedZombie.name = "Felix" + Random.Range(0, 150);
                setModel(spawnedZombie, Random.Range(0, m_models.Length));
                addInfoPanel(spawnedZombie);
                SpawnManager.instance.addZombie(spawnedZombie);
            }
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
