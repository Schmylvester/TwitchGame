using System.Collections;

// i have 1990876496 health
public class Xavier : UnitClass
{
    protected override string getFile() { return "xavier"; }
    public override IEnumerator usePower(PlayerAI myAi, Player player)
    {
        player.shoot(true);
        yield return null;
    }
}
