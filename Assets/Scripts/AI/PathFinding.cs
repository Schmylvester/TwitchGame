using System.Collections.Generic;
using UnityEngine;

public struct NodeValue
{
    public Node node;
    public List<Node> path;
}

public class PathFinding
{
    bool m_atDestination = false;
    int m_index = 0;
    Node m_whereIAm = null;
    Node[] m_path;

    List<NodeValue> m_openList = new List<NodeValue>();
    List<Node> m_closedList = new List<Node>();

    public Node currentNode { get { return m_whereIAm; } }

    public void spawn(Node _spawnNode)
    {
        m_whereIAm = _spawnNode;
    }

    /// <summary>
    /// advances on the path
    /// </summary>
    /// <returns>whether we have reached the destination</returns>
    public bool advancePath()
    {
        m_whereIAm = m_path[m_index];
        if (m_index + 1 >= m_path.Length || m_atDestination)
        {
            m_atDestination = true;
            return true;
        }
        else
        {
            m_index++;
            return false;
        }
    }

    public Vector3 getTargetPos(out bool destinationArrived)
    {
        destinationArrived = false;
        if (m_path == null)
        {
            return Vector3.zero;
        }
        else if (m_path.Length == 0)
        {
            return Vector3.zero;
        }
        else
        {
            if (m_index == m_path.Length - 1)
            {
                DestinationNode destinationNode = m_path[m_index] as DestinationNode;
                if (destinationNode)
                {
                    destinationArrived = true;
                    return destinationNode.getPointInRoom();
                }
                return Vector3.zero;
            }
            else
            {
                return m_path[m_index].transform.position;
            }
        }
    }

    public void createPath(Node destination)
    {
        m_atDestination = false;
        m_index = 0;
        if (m_whereIAm == destination)
            return;

        m_openList.Add(new NodeValue() { node = m_whereIAm, path = new List<Node>() });
        int loopBreaker = 0;
        while (loopBreaker < 100000)
        {
            foreach (Node node in m_openList[0].node.getConnectedNodes())
            {
                if (!m_openList.Find((NodeValue n) => n.node == node).node || !m_closedList.Contains(node))
                {
                    List<Node> path = new List<Node>();
                    m_openList[0].path.ForEach((Node n) => path.Add(n));
                    path.Add(m_openList[0].node);
                    m_openList.Add(new NodeValue() { node = node, path = path });
                }
                if (node == destination)
                {
                    m_openList[0].path.Add(m_openList[0].node);
                    m_openList[0].path.Add(node);
                    m_path = new Node[m_openList[0].path.Count - 1];
                    m_openList[0].path.RemoveAt(0);
                    m_openList[0].path.CopyTo(m_path);

                    m_openList.Clear();
                    m_closedList.Clear();
                    return;
                }
            }
            m_closedList.Add(m_openList[0].node);
            m_openList.RemoveAt(0);
            sortOpenList();
        }

        Debug.LogError("Endless loop probably");
    }

    void sortOpenList()
    {
        bool listIsSorted = false;
        while (!listIsSorted)
        {
            listIsSorted = true;
            for (int i = 1; i < m_openList.Count; ++i)
            {
                if (m_openList[i - 1].path.Count > m_openList[i].path.Count)
                {
                    listIsSorted = false;
                    NodeValue temp = m_openList[i - 1];
                    m_openList[i - 1] = m_openList[i];
                    m_openList[i] = temp;
                }
            }
        }
    }

    void LogPath()
    {
        string s = "";
        foreach (Node n in m_path)
        {
            s += n.gameObject.name + " - ";
        }
        Debug.Log(s);
    }
}
