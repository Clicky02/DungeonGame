using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : HealthEntity
{

    Random r = new Random();
    public int sightRange = 5;
    public int followRange = 7;
    
    public HealthEntity previousTarget = null;

    // Start is called before the first frame update
    public override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        leftAnimationHash = Animator.StringToHash("GhostLeft");
        rightAnimationHash = Animator.StringToHash("GhostRight");
        upAnimationHash = Animator.StringToHash("GhostBack");
        downAnimationHash = Animator.StringToHash("GhostFront");
        actionSpeed = 0.25f;
        actionTime = 1;
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    public override void Act()
    {
        HealthEntity target = null;
        int cDistX = 1000;
        int cDistY = 1000;
        int cEX = 1000;
        int cEY = 1000;
        foreach(Entity e in Control.c.entities.Values)
        {
            HealthEntity h = e as HealthEntity;
            if (h != null && h.team != team)
            {
                int distX = h.tilePos.x - tilePos.x;
                int distY = h.tilePos.y - tilePos.y;
                if ((Mathf.Abs(distX) <= sightRange && Mathf.Abs(distY) <= sightRange) ||  (h == previousTarget && (Mathf.Abs(distX) <= followRange && Mathf.Abs(distY) <= followRange)))
                {
                    Debug.Log(h == previousTarget);
                    int eX = distX*((int)direction.x) + distY*((int)direction.y);
                    int eY = distY* ((int)direction.x) + distX* ((int)direction.y);
                    if (((0 < eX && Mathf.Abs(eY) <= eX && Physics2D.Linecast(h.transform.position, transform.position).collider == null)) && eX < cEX)
                    {
                        target = h;
                        cDistX = distX;
                        cDistY = distY;
                        cEX = eX;
                        cEY = eY;
                    }
                    else if (h == previousTarget && eX < cEX)
                    {
                        target = h;
                        cDistX = distX;
                        cDistY = distY;
                        cEX = eX;
                        cEY = eY;
                    }
                }
            }
        }

        if (target != null)
        {
            previousTarget = target;
            bool b = Mathf.Abs(cDistX) > Mathf.Abs(cDistY);

            for (int  i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        Vector3 v0 = (new Vector3(b ? cDistX : 0, b ? 0 : cDistY, 0).normalized);
                        if (Control.c.IsTile(tilePos + Vector3Int.RoundToInt(v0)))
                        {
                            Move(v0);
                            i = 4;
                        }
                        break;
                    case 1:
                        Vector3 v1 = (new Vector3(b ? 0 : cDistX, b ? cDistY : 0, 0).normalized);
                        if (Control.c.IsTile(tilePos + Vector3Int.RoundToInt(v1)))
                        {
                            Move(v1);
                            i = 4;
                        }
                        break;
                    case 2:
                        Vector3 v2 = (new Vector3(b ? 0 : -cDistX, b ? -cDistY : 0, 0).normalized);
                        if (Control.c.IsTile(tilePos + Vector3Int.RoundToInt(v2)))
                        {
                            Move(v2);
                            i = 4;
                        }
                        break;
                    case 3:
                        Vector3 v3 = (new Vector3(b ? -cDistX : 0, b ? 0 : -cDistY, 0).normalized);
                        if (Control.c.IsTile(tilePos + Vector3Int.RoundToInt(v3)))
                        {
                            Move(v3);
                            i = 4;
                        }
                        break;
                    default:
                        MoveRandomDirection();
                        break;


                }

            }
        }
        else
        {
            previousTarget = null;
            MoveRandomDirection();
        }

    }

    void MoveRandomDirection()
    {
        switch (Mathf.Floor(Random.value * 4))
        {
            case 0:
                Move(Vector3.up);
                break;
            case 1:
                Move(Vector3.down);
                break;
            case 2:
                Move(Vector3.right);
                break;
            case 3:
                Move(Vector3.left);
                break;
            default:
                Move(Vector3.down);
                break;
        }
    }
}
