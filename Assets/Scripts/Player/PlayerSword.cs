using UnityEngine;

public class PlayerSword : Sword
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeHit(Damage);
        }
    }
}