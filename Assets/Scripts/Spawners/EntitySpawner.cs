using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] protected Transform m_gameCanvas;
    [SerializeField] protected GameObject m_infoPanelPrefab;
    [SerializeField] protected GameStateManager m_gameStateManager = null;
    [SerializeField] protected RuntimeAnimatorController[] m_models = null;
    [SerializeField] protected GameObject m_entityPrefab = null;

    protected void setModel(GameObject _entity, int _index)
    {
        _entity.GetComponentInChildren<Animator>().runtimeAnimatorController = m_models[_index];
    }
}
