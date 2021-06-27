using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Node[] m_connectedNodes;

    void Start()
    {
        foreach(Node node in m_connectedNodes)
            Debug.DrawLine(transform.position, node.transform.position, Random.ColorHSV(), 10.0f);
    }
}
