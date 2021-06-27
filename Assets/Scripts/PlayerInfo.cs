using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : EntityInfo
{
    [SerializeField] Text m_nameTag = null;

    public void setTrackPlayer(Transform _player, string _name, Color _nameColour)
    {
        m_nameTag.text = _name;
        m_nameTag.color = _nameColour;
        base.setTrackPlayer(_player);
    }
}
