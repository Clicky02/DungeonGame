using UnityEngine;

public class BoltProjectile : Projectile
{

    public override void OnTriggerEnter2D(Collider2D collision)
    {

        Entity e = collision.gameObject.GetComponent(typeof(Entity)) as Entity;
        if (e != null)
        {
            if (e is HealthEntity && e != caster)
            {
                new DamageEvent(e as HealthEntity, caster, damage, "projectile", crit).Invoke();
                new MoveEvent(e as HealthEntity, this.direction, (e as HealthEntity).direction).Invoke();
                Destroy(this.gameObject);
            }
            else if (e is Projectile)
            {
                Destroy(this.gameObject);
            }

        }
        else
        {
            Destroy(this.gameObject);
        }

    }


}