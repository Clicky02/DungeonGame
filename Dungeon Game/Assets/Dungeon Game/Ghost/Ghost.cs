using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : HealthEntity
{

    Random r = new Random();
    public int sightRange = 5;

    // Start is called before the first frame update
    public override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        leftAnimationHash = Animator.StringToHash("GhostLeft");
        rightAnimationHash = Animator.StringToHash("GhostRight");
        upAnimationHash = Animator.StringToHash("GhostBack");
        downAnimationHash = Animator.StringToHash("GhostFront");
        actionSpeed = 0.5f;
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
        foreach(Entity e in myDictionary.Values)
        {
            HealthEntiy h = e as HealthEnity;
            if (h != null && h.team != team)
            {
                int distX = h.tilePos.x - tilePos.x;
                int distY = h.tilePos.y - tilePos.y;
                if (Mathf.Abs(distX) <= sightRange && Mathf.Abs(distY) <= sightRange)
                {
                    eX = distX*direction.x + distY*direction.y;
                    eY = distY*direction.x + distX*direction.y;
                    if (0 < eX && Mathf.abs(eY) <= eX && eX < cEX)
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
