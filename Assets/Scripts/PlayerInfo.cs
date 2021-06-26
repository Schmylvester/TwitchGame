using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : EntityInfo
{
    [SerializeField] Text m_nameTag = null;

    public void setTrackPlayer(Transform _player, string _name)
    {
        m_nameTag.text = _name;
        base.setTrackPlayer(_player);
    }
}
