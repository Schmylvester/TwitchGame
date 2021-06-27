using UnityEngine;

// wow what if you can heal
public class Rich : UnitClass
{
    public override float getBaseHealth() { return 150.0f; }
    public override float getMovingAccuracy() { return 0.85f; }
    public override float getRunningDodge() { return 0.4f; }
    public override float getRunSpeed() { return 0.4f; }
    public override float getStaticAccuracy() { return 0.9f; }
    public override float getStaticDodge() { return 0.01f; }
    public override float getWalkingDodge() { return 0.03f; }
    public override float getWalkSpeed() { return 0.2f; }
    public override float getFireRate() { return 1.0f; }
    public override void usePower()
    {
        foreach (GameObject gameObject in SpawnManager.instance.getPlayers())
        {
            gameObject.GetComponent<Player>().heal(0.2f);
        }
    }
}
