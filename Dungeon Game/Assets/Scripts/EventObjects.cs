using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject
{
    protected bool canceled = false;

    public abstract bool Invoke();
    public abstract void Cancel();

}

public class AttackEvent : EventObject
{
    public HealthEntity target;
    public HealthEntity attacker;

    //Attacker stats
    public float attack;
    public float accuracy;
    public float critChance;
    public float critMultiplier;

    //Target stats
    public float defense;
    public float evadeChance;

    public bool pAttacker = false; //If attacker is the player
    public bool pTarget = false; //If target is the player
    
    public string cause = "attack";


    public AttackEvent(HealthEntity target, HealthEntity attacker)
    {
        this.target = target;
        this.attacker = attacker;

        attack = attacker.attack;
        accuracy = attacker.accuracy;
        critChance = attacker.critChance;
        critMultiplier = attacker.critMultiplier;

        defense = target.defense;
        evadeChance = target.evadeChance;

        if (target is Player) pTarget = true;
        if (attacker is Player) pAttacker = true;
    }
    
    public AttackEvent(HealthEntity target, HealthEntity attacker, float damage, string cause)
    {
        this.target = target;
        this.attacker = attacker;

        attack = damage;
        accuracy = attacker.accuracy;
        critChance = attacker.critChance;
        critMultiplier = attacker.critMultiplier;

        defense = target.defense;
        evadeChance = target.evadeChance;

        if (target is Player) pTarget = true;
        if (attacker is Player) pAttacker = true;
        
        this.cause = cause;
    }

    public override void Cancel()
    {
        
    }

    public override bool Invoke()
    {
        bool doesHit = GameData.data.DoesSucceed(accuracy, pAttacker) && !GameData.data.DoesSucceed(evadeChance, pTarget);
        int numCrits = 0;

        attack -= defense;
        if (attack < 1) attack = 1;
        
        if (doesHit)
        {
            numCrits = GameData.data.DoesSucceed(critChance, pTarget || pAttacker, pTarget);
            attack *= Mathf.Pow(critMultiplier, numCrits);
        }
        else
        {
            attack = 0;
        }
        new DamageEvent(target, attacker, attack, cause, numCrits).Invoke();
        return true;
    }
    
    public int GetCrit(float critChance) 
    {
        int crits = 0;
        
        if (critChance > 1)
        {
            critChance -= 1;
            crits += GetCrit(critChance);
            return crits + 1;
        }
        
        if (GameData.data.DoesSucceed(critChance, pTarget || pAttacker, pTarget))
            return 1;
        else
            return 0;
    }
}

public class SpellHitEvent : EventObject
{
    public HealthEntity target;
    public HealthEntity attacker;
    public float damage;


    public SpellHitEvent(HealthEntity target, HealthEntity attacker, float damage)
    {
        this.target = target;
        this.attacker = attacker;
        this.damage = damage;
    }

    public override void Cancel()
    {

    }

    public override bool Invoke()
    {
        new AttackEvent(target, attacker, damage, "spell").Invoke();
        return true;
    }
}



public class TrapDamageEvent : EventObject
{
    public override void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public override bool Invoke()
    {
        throw new System.NotImplementedException();
    }
}





public class DamageEvent : EventObject
{
    public HealthEntity target;
    public Entity damager;
    public float damage;
    public string method;
    public float numCrits;

    public DamageEvent(HealthEntity target, Entity damager, float damage, string method, float numCrits)
    {
        this.target = target;
        this.damager = damager;
        this.damage = damage;
        this.method = method;
        this.numCrits = numCrits;
    }

    public override void Cancel()
    {

    }

    public override bool Invoke()
    {
        if (damager is HealthEntity)
            (damager as HealthEntity).InvokeDamageDealt(this);

        target.InvokeDamageTaken(this);



        if (target.Damage(damage))
        {
            if (damager == LevelData.data.p)
            {
                GameData.data.AddExperience(target.deathExperience);
            }
        }
        target.CreateDamageNumber(damage, numCrits);
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
        this.damage = this.ability.damage * p.attack;
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

    public MoveEvent(HealthEntity e, Vector3 dir, Vector3 rot)
    {
        this.e = e;
        this.x = (int)dir.x;
        this.y = (int)dir.y;
        rotation = rot;
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
            if (rotation.x >= 1)
            {
                e.anim.Play(e.rightAnimationHash);
            }
            else if (rotation.x <= -1)
            {
                e.anim.Play(e.leftAnimationHash);
            }
            else if (rotation.y >= 1)
            {
                e.anim.Play(e.upAnimationHash);
            }
            else if (rotation.y <= -1)
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
                    e.movement = new Movement(newTilePos, duration, e);
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

