using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] protected Node[] m_connectedNodes;

    private void OnDrawGizmosSelected()
    {
        //DebugDraw();
    }

    protected virtual void DebugDraw()
    {
        foreach (Node node in m_connectedNodes)
            Debug.DrawLine(transform.position, node.transform.position, Color.cyan, 0.0f);
    }
}
