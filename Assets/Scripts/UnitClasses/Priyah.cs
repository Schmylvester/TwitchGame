// shoot de gun and move faste
public class Priyah : UnitClass
{
    public override float getBaseHealth() { return 100.0f; }
    public override float getMovingAccuracy() { return 0.9f; }
    public override float getRunningDodge() { return 0.7f; }
    public override float getRunSpeed() { return 0.5f; }
    public override float getStaticAccuracy() { return 1.0f; }
    public override float getStaticDodge() { return 0.02f; }
    public override float getWalkingDodge() { return 0.05f; }
    public override float getWalkSpeed() { return 0.25f; }
    public override float getFireRate() { return 0.9f; }
    public override void usePower() { return; } 
}
