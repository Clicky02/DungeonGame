using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : HealthEntity
{

    Random r = new Random();

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
