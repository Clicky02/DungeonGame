using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HealthEntity : Entity
{
    protected float maxHealth = 20;
    protected float maxMana = 0;
    public float mana = 0;
    public float manaRegen = 0;
    public float health = 20;
    public float damage = 3;

    public float defense = 0;
    public int level = 0;

    public bool frozen = false;
   
    public Movement movement = null;
    public ColorChange cc = null;

    protected float actionTime = 0.0F;
    public float actionSpeed = -1F;

    public Vector3 direction = Vector3.down;


    public delegate void AbilityCast(AbilityCastEvent e);
    public event AbilityCast onAbilityCast;

    public delegate void DamageDealt(DamageEvent e);
    public event DamageDealt onDamageDealt;

    public delegate void KillEnemy(Player self, HealthEntity target);
    public event KillEnemy onKill;

    public delegate void DamageTaken(DamageEvent e);
    public event DamageTaken onDamageTaken;




    // Start is called before the first frame update
    public override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        maxMana = mana;
        maxHealth = health;
        leftAnimationHash = Animator.StringToHash("WizardLeft");
        rightAnimationHash = Animator.StringToHash("WizardRight");
        upAnimationHash = Animator.StringToHash("WizardBack");
        downAnimationHash = Animator.StringToHash("WizardFront");
        if (actionSpeed == -1F)
        {
            actionSpeed = 10F;
        }
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    public override void Update()
    {
        ManaUpdate(Time.deltaTime);
        if (cc != null)
        {
            if (cc.Tick(Time.deltaTime))
            {
                cc = null;
            }
        }
        if (movement != null)
        {
            if (movement.Tick(Time.deltaTime))
            {
                movement = null;
            }
        }
        else if (!frozen)
        {
            actionTime += Time.deltaTime;

            if (actionTime > actionSpeed)
            {
                actionTime = 0;
                Act();
            }
        }

    }

    public virtual void ManaUpdate(float deltaTime)
    {
        if (mana != maxMana)
        {
            float manaIncrease = deltaTime * manaRegen;
            mana += manaIncrease;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
        }
    }

    public virtual void Act()
    {

    }

    public virtual void Attack(HealthEntity e, Vector3 dir)
    {
        movement = new Movement("attack", dir, 0.1f, this);
        new DamageEvent(e, this, damage, "melee", false).Invoke();
    }

    public virtual bool Damage(float damage)
    { 
        health -= damage;
        
        if (health <= 0)
        {
            Kill();
            return true;
        }
        else
        {
            cc = new ColorChange(this, new Color(baseColor.r + 0.3f, baseColor.g - 0.3f, baseColor.b - 0.3f),
                0.3f, sr);
        }
        return false;
    }

    public virtual void CreateDamageNumber(float damage, bool crit)
    {
        GameObject g = Instantiate(ResourceHolder.r.damageNumber, this.transform);
        g.transform.SetParent(transform.parent);
        DamageNumber dn = g.GetComponent<DamageNumber>();
        dn.damage = Mathf.RoundToInt(damage);
        if (crit) dn.c = new Color(255, 215, 0);
    }

    public virtual void Kill()
    {
        if (c.IsTile(tilePos))
        {
            c.RemoveEntity(tilePos);
        }
        Destroy(this.gameObject);
    }

    public virtual void Rotate(Vector3 dir)
    {
        if (!frozen)
        {
            int x = (int)dir.normalized.x;
            int y = (int)dir.normalized.y;

            if (x == 1)
            {
                anim.Play(rightAnimationHash);
            }
            else if (x == -1)
            {
                anim.Play(leftAnimationHash);
            }
            else if (y == 1)
            {
                anim.Play(upAnimationHash);
            }
            else if (y == -1)
            {
                anim.Play(downAnimationHash);
            }
            direction = dir;
        }
    }

    public virtual void Move(Vector3 dir)
    {

        MoveEvent e = new MoveEvent(this, dir);
        e.Invoke();


    }

    public void InvokeDamageTaken(DamageEvent e)
    {
        onDamageTaken?.Invoke(e);
    }

    public void InvokeDamageDealt(DamageEvent e)
    {
        onDamageDealt?.Invoke(e);
    }

    public void InvokeKillEnemy()
    {

    }

    public void InvokeUseAbility(AbilityCastEvent e)
    {
        onAbilityCast?.Invoke(e);
    }
}