using System.IO;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public float
        baseHealth,
        walkSpeed,
        runSpeed,
        staticAccuracy,
        movingAccuracy,
        runningDodge,
        walkingDodge,
        staticDodge,
        fireRate;
}

public abstract class UnitClass
{
    protected UnitStats m_stats;

    public UnitClass()
    {
        string filePath = Application.persistentDataPath + '/' + getFile();
        if (File.Exists(filePath))
        {
            StreamReader stream = new StreamReader(filePath);
            parseJson(stream.ReadToEnd());
            stream.Close();
        }
        else
        {
            Debug.LogError("Bad file path");
        }
    }

    protected virtual void parseJson(string json)
    {
        m_stats = JsonUtility.FromJson<UnitStats>(json);
    }

    public float getBaseHealth() { return m_stats.baseHealth; }
    public float getWalkSpeed() { return m_stats.walkSpeed; }
    public float getRunSpeed() { return m_stats.runSpeed; }
    public float getStaticAccuracy() { return m_stats.staticAccuracy; }
    public float getMovingAccuracy() { return m_stats.movingAccuracy; }
    public float getRunningDodge() { return m_stats.runningDodge; }
    public float getWalkingDodge() { return m_stats.walkingDodge; }
    public float getStaticDodge() { return m_stats.staticDodge; }
    public float getFireRate() { return m_stats.fireRate; }
    public abstract IEnumerator usePower(PlayerAI ai, Player player);
    protected abstract string getFile();
}