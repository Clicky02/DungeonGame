using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{

    private GameObject fireball;

    public Fireball()
    {
        name = "Fireball";
        description = "Hurl a fireball at your enemies.";
        lore = "A time tested classic among wizards.";
        manaCost = 3;
        damage = 1.5f;
        imageName = "fireball0";
        cooldown = 0.3f;
    }

    public override void Instantiate(Player player)
    {
        fireball = Resources.Load("fireball", typeof(GameObject)) as GameObject;
        p = player;
    }

    // Start is called before the first frame update
    public override bool Cast(float damage, float manaCost, bool crit)
    {
        bool cast = p.UseMana(manaCost);
        if (cast)
        {
            Projectile f = GameObject.Instantiate(fireball).GetComponent<Projectile>();
            f.tilePos = new Vector3Int(p.tilePos.x, p.tilePos.y, 0);
            f.caster = p;
            f.c = Control.c;
            f.direction = p.direction;
            f.crit = crit;
            f.damage = damage;
            p.SetCooldown(abilityNumber, cooldown);
        }
        return cast;
        
    }
}

public class MagicBolt : Ability
{

    private GameObject magicBolt;

    public MagicBolt()
    {
        name = "Magic Bolt";
        description = "Fire a nuetral projectile with limited range.";
        lore = "Probably the easiest spell in existence.";
        manaCost = 0;
        damage = 1f;
        imageName = "magicBolt";
        type = "basic";
        cooldown = 1.8f;
    }

    public override void Instantiate(Player player)
    {

        magicBolt = Resources.Load("MagicBolt", typeof(GameObject)) as GameObject;
        p = player;
    }

    // Start is called before the first frame update
    public override bool Cast(float damage, float manaCost, bool crit)
    {
        bool cast = p.UseMana(manaCost);
        if (cast)
        {
            Projectile f = GameObject.Instantiate(magicBolt).GetComponent<Projectile>();
            f.tilePos = new Vector3Int(p.tilePos.x, p.tilePos.y, 0);
            f.range = 3;
            f.caster = p;
            f.c = Control.c;
            f.direction = p.direction;
            f.crit = crit;
            f.damage = damage;
            p.SetCooldown(abilityNumber, cooldown);
        }
        return cast;

    }
}

public class Lightning : Ability
{

    private GameObject lightning;

    public Lightning()
    { 
        name = "Lightning";
        description = "Shock enemies at a close proximity.";
        lore = "Warning: This spell will make you feel like a sith lord.";
        manaCost = 8;
        damage = 0.85f;
        imageName = "lightning";
        cooldown = 1f;
    }

    public override void Instantiate(Player player)
    {
        lightning = Resources.Load("Lightning", typeof(GameObject)) as GameObject;
        p = player;
    }

    // Start is called before the first frame update
    public override bool Cast(float damage, float manaCost, bool crit)
    {
        bool cast = p.UseMana(manaCost);
        if (cast)
        {
            LightningScript f = GameObject.Instantiate(lightning).GetComponent<LightningScript>();
            f.caster = p;
            f.direction = p.direction;
            f.crit = crit;
            f.damage = damage;
            p.SetCooldown(abilityNumber, cooldown);
        }
        return cast;

    }
}

public class EnergyWave : Ability
{

    private GameObject lightning;

    public EnergyWave()
    {
        name = "Energy Wave";
        description = "Shock enemies at a close proximity.";
        lore = "Warning: This spell will make you feel like a sith lord.";
        manaCost = 8;
        damage = 0.85f;
        imageName = "lightning";
        cooldown = 1f;
    }

    public override void Instantiate(Player player)
    {
        lightning = Resources.Load("Lightning", typeof(GameObject)) as GameObject;
        p = player;
    }

    // Start is called before the first frame update
    public override bool Cast(float damage, float manaCost, bool crit)
    {
        bool cast = p.UseMana(manaCost);
        if (cast)
        {
            LightningScript f = GameObject.Instantiate(lightning).GetComponent<LightningScript>();
            f.caster = p;
            f.direction = p.direction;
            f.crit = crit;
            f.damage = damage;
            p.SetCooldown(abilityNumber, cooldown);
        }
        return cast;

    }
}
