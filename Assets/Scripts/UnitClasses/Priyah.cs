using System.Collections;
using UnityEngine;

// shoot de gun and move faste

[System.Serializable]
public class PriyahStats
{
    public float
        upgradedFireRate,
        upgradedAccuracy,
        powerTimer;
}

public class Priyah : UnitClass
{
    protected override string getFile() { return "priyah"; }
    PriyahStats m_priyahStats;

    protected override void parseJson(string json)
    {
        m_priyahStats = JsonUtility.FromJson<PriyahStats>(json);
        base.parseJson(json);
    }

    public override IEnumerator usePower(PlayerAI myAi, Player player)
    {
        myAi.setAIBehaviour(AIBehaviour.HOLD);
        myAi.lockBehaviour();
        UnitStats originalStats = JsonUtility.FromJson<UnitStats>(JsonUtility.ToJson(m_stats));
        m_stats = new UnitStats()
        {
            baseHealth = m_stats.baseHealth,
            staticAccuracy = m_priyahStats.upgradedAccuracy,
            staticDodge = m_stats.staticDodge,
            fireRate = m_priyahStats.upgradedFireRate
        };
        yield return new WaitForSeconds(m_priyahStats.powerTimer);
        myAi.unlockBehaviour();
        m_stats = JsonUtility.FromJson<UnitStats>(JsonUtility.ToJson(originalStats));
        yield return null;
    }
}
