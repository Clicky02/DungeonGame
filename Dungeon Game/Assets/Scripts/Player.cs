using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : HealthEntity
{


    public GameObject healthBar;
    protected RectTransform hBR; //health Bar Rec
    protected float hBRMaxWidth = 0;

    public GameObject manaBar;
    protected RectTransform mBR; //mana Bar Rect
    protected float mBRMaxWidth = 0;

    protected string basicAbility = "MagicBolt";
    protected List<string> abilityStrings = new List<string>() { "Fireball", "Lightning" };
    public List<Ability> abilities = new List<Ability>();
    public List<Button> buttons = new List<Button>();

    protected List<Perk> perks = new List<Perk>();

    public float physicalDamage = 0;
    public float magicDamage = 0;


    public override void Start()
    {
        team = -1;
        PullStats();
        DontDestroyOnLoad(gameObject);

        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        leftAnimationHash = Animator.StringToHash("WizardLeft");
        rightAnimationHash = Animator.StringToHash("WizardRight");
        upAnimationHash = Animator.StringToHash("WizardBack");
        downAnimationHash = Animator.StringToHash("WizardFront");

        baseColor = GetComponent<SpriteRenderer>().color;

        if (LevelData.data.p  == null) {
            Debug.Log(1); 
            mana = maxMana;
            health = maxHealth;  
        } else {
            LevelData.data.SetNewPlayer(this);
        }
        LevelData.data.p = this;
            
        Connect();
    }
    
    //Things required to connect this class with outside classes
    public void Connect() {
            //Health Bar
            hBR = healthBar.GetComponent<RectTransform>();
            hBRMaxWidth = hBR.sizeDelta.x;
            hBR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hBRMaxWidth * (float)(health / maxHealth));

            //Mana Bar
            mBR = manaBar.GetComponent<RectTransform>();
            mBRMaxWidth = mBR.sizeDelta.x;
            mBR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mBRMaxWidth * (float)(mana / maxMana));



            LoadAbilities();
    }

    public void OnDisable()
    {
        foreach (Perk p in perks)
        {
            p.DeLoad(this);
        }
    }

    public void PullStats()
    {
        GameData data = GameData.data;

        maxHealth = data.baseHealth;
        maxMana = data.baseMana;
        physicalDamage = data.weapon.physicalAttack;
        magicDamage = data.weapon.magicAttack;

        List<Armour> equippedArmour = data.GetArmourList();
        foreach (Armour a in equippedArmour)
        {
            maxHealth += a.healthBoost;
            maxMana += a.manaBoost;
            defense += a.defense;

            foreach (string perkName in a.perks)
            {
                Type perkType = Type.GetType(perkName);
                Perk p = (Perk)Activator.CreateInstance(perkType);
                p.Load(this);
            }
        }

        maxHealth += data.weapon.healthBoost;
        maxMana += data.weapon.manaBoost;

        basicAbility = data.basicAbility;
        abilityStrings = data.equippedAbilities;
    }

    public void LoadAbilities()
    {
        SetButton(basicAbility, 0);
        int abilityNumber = 1;
        foreach (string s in abilityStrings)
        {
            if (abilityNumber < 5)
            {
                SetButton(s, abilityNumber);
            }
            abilityNumber += 1;
        }
    }

    void SetButton(string s, int abilityNumber)
    {
        if (s != "")
        {
            Type t = Type.GetType(s);
            Ability a = (Ability)Activator.CreateInstance(t);
            a.Instantiate(this);
            a.SetButton(buttons[abilityNumber], abilityNumber);
            abilities.Add(a);
        }
        else
        {
            abilities.Add(null);
        }
        
    }

    public override void Kill()
    {
        //TODO: Death Screen
    }

    public override bool Damage(float damage)
    {
        bool b = base.Damage(damage);
        hBR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hBRMaxWidth * (float)(health / maxHealth));
        return b;
    }

    public void UseAbility(int i)
    {
        if (!frozen)
        {
            AbilityCastEvent e = new AbilityCastEvent(this, i);
            InvokeUseAbility(e);
            e.Invoke();
        }
    }

    public bool UseMana(float usedMana)
    {
        if (mana > usedMana)
        {
            mana -= usedMana;
            mBR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mBRMaxWidth * (float)(mana / maxMana));
            return true;
        }
        return false;
    }

    public override void ManaUpdate(float deltaTime)
    {
        base.ManaUpdate(deltaTime);
        mBR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mBRMaxWidth * (float)(mana / maxMana));
    }

    public override void Attack(HealthEntity e, Vector3 dir)
    {
        Debug.Log("");
        float d = damage;
        new DamageEvent(e, this, d, "melee", false).Invoke();
        movement = new Movement("attack", dir, 0.1f, this);
    }

    public override void CreateDamageNumber(float damage, bool crit)
    {
        GameObject g = Instantiate(ResourceHolder.r.damageNumber, this.transform);
        g.transform.SetParent(transform.parent);
        DamageNumber s = g.GetComponent<DamageNumber>();
        s.damage = Mathf.RoundToInt(damage);
        s.c = new Color(1, 0.3f, 0.3f);
    }

    public void SetCooldown(int num, float time)
    {
        buttons[num].interactable = false;
        StartCoroutine(EndCooldown(num, time));
    }

    IEnumerator EndCooldown(int num, float time)
    {
        yield return new WaitForSeconds(time);
        buttons[num].interactable = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("h3");
    }

}
