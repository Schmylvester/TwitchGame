using System.Collections;
using UnityEngine;

// wow what if you can heal

[System.Serializable]
public class RichStats
{
    public float healAmount;
}

public class Rich : UnitClass
{
    RichStats m_richStats;

    protected override void parseJson(string json)
    {
        m_richStats = JsonUtility.FromJson<RichStats>(json);
        base.parseJson(json);
    }

    protected override string getFile() { return "rich"; }

    public override IEnumerator usePower(PlayerAI myAi, Player player)
    {
        foreach (GameObject gameObject in SpawnManager.instance.getPlayers())
        {
            gameObject.GetComponent<Player>().heal(m_richStats.healAmount);
        }
        yield return null;
    }
}
