using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DestinationNode : Node
{
    [SerializeField] Rect m_roomBounds;

    public Rect getRoomBounds { get {  return m_roomBounds; } }

    public Vector3 getPointInRoom()
    {
        float x = Random.Range(m_roomBounds.x, m_roomBounds.x + m_roomBounds.width);
        float y = Random.Range(m_roomBounds.y, m_roomBounds.y + m_roomBounds.height);
        return new Vector3(x, y);
    }

    protected override void DebugDraw()
    {
        Debug.DrawLine(new Vector3(m_roomBounds.xMin, m_roomBounds.yMin), new Vector3(m_roomBounds.xMax, m_roomBounds.yMin), Color.yellow, 0.0f);
        Debug.DrawLine(new Vector3(m_roomBounds.xMax, m_roomBounds.yMin), new Vector3(m_roomBounds.xMax, m_roomBounds.yMax), Color.yellow, 0.0f);
        Debug.DrawLine(new Vector3(m_roomBounds.xMax, m_roomBounds.yMax), new Vector3(m_roomBounds.xMin, m_roomBounds.yMax), Color.yellow, 0.0f);
        Debug.DrawLine(new Vector3(m_roomBounds.xMin, m_roomBounds.yMax), new Vector3(m_roomBounds.xMin, m_roomBounds.yMin), Color.yellow, 0.0f);

        base.DebugDraw();
    }
}
