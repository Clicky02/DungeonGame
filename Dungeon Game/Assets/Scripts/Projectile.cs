using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{

    public Movement movement = null;
    public Vector3 direction = Vector3.up;
    public float speed = 0.02f;
    public float damage = 5;
    public bool crit = false;
    public int range = 10;
    protected int distanceTraveled = 0;
    protected int x;
    protected int y;

    protected int phase = 0; 
    //This is whether it is moving to the next tile or towards the middle
    //It checks to stop at a wall or something the moment it reaches the next tile
    //It doesn't damage until it gets to the center of the tile
    // 0 = to next tile, 1 = to center

    public HealthEntity caster;

    // Start is called before the first frame update
    public override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(tilePos.x, tilePos.y-0.3f, tilePos.y + 0.1f);
        transform.rotation = Quaternion.Euler(0,0,Vector3.SignedAngle(Vector3.up, direction, Vector3.forward));
        movement = new Movement(Vector3.up, speed*2*range, (float)range, this);
        x = (int)direction.normalized.x;
        y = (int)direction.normalized.y;
        Control.c.AddProjectile(tilePos, this);
        rb = GetComponent<Rigidbody2D>();
        baseColor = sr.color;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (movement != null)
        {

            if (movement.Tick2(Time.deltaTime))
            {
 
                Destroy(this.gameObject);   
                
            }
        }

    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Entity e = collision.gameObject.GetComponent(typeof(Entity)) as Entity;
        if (e != null)
        {
            if (e is HealthEntity && e != caster)
            {
                new DamageEvent(e as HealthEntity, caster, damage, "projectile", crit).Invoke();
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



