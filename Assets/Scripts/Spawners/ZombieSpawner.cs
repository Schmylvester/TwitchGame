using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : EntitySpawner
{
    [SerializeField] float m_spawnRate = 1.0f;
    [SerializeField] protected Transform m_spawnPosParent = null;

    private void Start()
    {
        StartCoroutine(spawnZombies());
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
