using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject
{
    protected bool canceled = false;

    public abstract bool Invoke();
    public abstract void Cancel();

}


public class DamageEvent : EventObject
{
    public HealthEntity target;
    public Entity damager;
    public float defense;
    public float damage;
    public string method;
    public bool crit;

    public DamageEvent(HealthEntity target, Entity damager, float damage, string method, bool crit)
    {
        this.target = target;
        this.damager = damager;
        this.defense = target.defense;
        this.damage = damage;
        this.method = method;
        this.crit = crit;
    }

    public override void Cancel()
    {

    }

    public override bool Invoke()
    {
        if (damager is HealthEntity)
        {
            (damager as HealthEntity).InvokeDamageDealt(this);
        }

        target.InvokeDamageTaken(this);

        float d = damage - defense;
        if (d < 1)
        {
            d = 1;
        }



        target.Damage(d);
        target.CreateDamageNumber(d, crit);
        return true;

    }
}

public class AbilityCastEvent : EventObject
{
    public Player p;
    public Ability ability;

    public float damage;
    public float manaCost;
    public bool crit = false;

    public int abilityNumber;


    public AbilityCastEvent(Player p, int ability)
    {
        this.p = p;
        this.ability = p.abilities[ability];
        this.damage = this.ability.damage * p.magicDamage;
        this.manaCost = this.ability.manaCost;
        abilityNumber = ability;
    }

    public override void Cancel()
    {

    }

    public override bool Invoke()
    {
        ability.Cast(damage, manaCost, crit);
        return true;
    }
}

public class MoveEvent : EventObject
{
    public HealthEntity e;
    public int x;
    public int y;
    public Vector3 rotation;
    public float duration = 0.2f;


    public MoveEvent(HealthEntity e, Vector3 dir)
    {
        this.e = e;
        this.x = (int)dir.x;
        this.y = (int)dir.y;
        rotation = dir;
    }

    public override void Cancel()
    {
        canceled = true;
    }

    public override bool Invoke()
    {
        if (canceled) return false;
        if (!e.frozen)
        {
            e.direction = rotation;
            if (x >= 1)
            {
                e.anim.Play(e.rightAnimationHash);
            }
            else if (x <= -1)
            {
                e.anim.Play(e.leftAnimationHash);
            }
            else if (y >= 1)
            {
                e.anim.Play(e.upAnimationHash);
            }
            else if (y <= -1)
            {
                e.anim.Play(e.downAnimationHash);
            }

            Vector3Int newTilePos = new Vector3Int(e.tilePos.x + x, e.tilePos.y + y, 0);

            if (Control.c.IsTile(newTilePos))
            {
                if (!Control.c.entities.ContainsKey(newTilePos))
                {
                    Control.c.RemoveEntity(e.tilePos);
                    Control.c.SetEntity(newTilePos, e);
                    e.movement = new Movement("walk", new Vector3(x, y), duration, e);
                    e.tilePos = newTilePos;
                    InteractableTile t = Control.c.GetTrap(newTilePos);
                    if (t != null) t.Interact(this);
                }
                else if (Control.c.GetEntity(newTilePos) is HealthEntity)
                {
                    e.Attack(Control.c.GetEntity(newTilePos) as HealthEntity, new Vector3(x, y));
                }

            }
        }
        return true;
    }
}

