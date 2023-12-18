using UnityEngine;

public class Bomb : Trap
{
    protected override void KillPlayer(IPlayer player)
    {
        player.MakeDamage();
    }
}
