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
        transform.position = new Vector3(tilePos.x, tilePos.y, tilePos.y + 0.1f);
        transform.rotation = Quaternion.Euler(0,0,Vector3.SignedAngle(Vector3.up, direction, Vector3.forward));
        movement = new Movement(Vector3.up, speed, 0.5f, this);
        x = (int)direction.normalized.x;
        y = (int)direction.normalized.y;
        c.AddProjectile(tilePos, this);
        baseColor = sr.color;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (movement != null)
        {

            if (movement.Tick2(Time.deltaTime))
            {
                if (phase == 0)
                {

                    phase = 1;

                    Vector3Int newTilePos = new Vector3Int(tilePos.x + x, tilePos.y + y, 0);
                    if (c.layerTwo.GetTile(newTilePos) != null)
                    {
                        c.RemoveProjectile(tilePos, this);
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        distanceTraveled += 1;
                        c.RemoveProjectile(tilePos, this);
                        tilePos = newTilePos;
                        c.AddProjectile(tilePos, this);
                        movement = new Movement(Vector3.up, speed, 0.5f, this);

                    }
                }
                else if (phase == 1)
                {
                    phase = 0;

                    Entity e = c.GetEntity(tilePos);
                    if (c.IsTile(tilePos) && e != null && e != caster)
                    {
                        if (e is HealthEntity)
                        {
                            new DamageEvent(e as HealthEntity, caster, damage, "projectile", crit).Invoke();
                        }
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        movement = new Movement(Vector3.up, speed, 0.5f, this);
                    }

                    if (!(distanceTraveled < range))
                    {
                        Destroy(this.gameObject);
                    }
                }
 
                
                
            }
        }

    }
}
