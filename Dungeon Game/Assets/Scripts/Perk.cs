using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perk
{
    public abstract void Load(Player p);

    public abstract void DeLoad(Player p);

    public abstract string GetDescription();

    public abstract string GetName();

}

public class Vengeance : Perk
{
    public string name = "Vengeance";
    public string description = "Do increased damage to an enmey after taking damage from it.";

    private HealthEntity targetedEntity = null;
    private float cooldown = 0;

    public override void Load(Player p)
    {
        p.onDamageTaken += OnDamageTakenEffect;
        p.onDamageDealt += OnDamageDealtEffect;
    }

    public override void DeLoad(Player p)
    {
        p.onDamageTaken -= OnDamageTakenEffect;
        p.onDamageDealt -= OnDamageDealtEffect;
    }


    void OnDamageTakenEffect(DamageEvent e)
    {
        targetedEntity = (e.damager as HealthEntity);
    }

    void OnDamageDealtEffect(DamageEvent e)
    {
        if (e.target == targetedEntity && Time.time > cooldown)
        {
            cooldown = Time.time + 4;
            e.damage *= 1.5f;
            e.numCrits += 1;
        }
    }

    public override string GetName() { return name; }
    public override string GetDescription() { return description; }

}

public class Fierce : Perk
{
    public string name = "Fierce";
    public string description = "Fireballs will occasionally do extra damage.";

    private HealthEntity targetedEntity = null;
    private float cooldown = 0;

    public override void Load(Player p)
    {
        p.onAbilityCast += OnAbilityCast;
    }

    public override void DeLoad(Player p)
    {
        p.onAbilityCast -= OnAbilityCast;
    }


    void OnAbilityCast(AbilityCastEvent e)
    {
        if (Mathf.Floor(Random.value * 3) < 1)
        {
            e.damage *= 1.26f;
            e.numCrits += 1;
        }
    }

    public override string GetName() { return name; }
    public override string GetDescription() { return description; }

}
