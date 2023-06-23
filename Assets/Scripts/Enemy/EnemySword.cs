using UnityEngine;

public class EnemySword : Sword
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeHit(Damage);
        }
    }
}