using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Entity : MonoBehaviour
{ 
    public Control c;
    public Vector3Int tilePos = new Vector3Int(0, 0, 0);

    public Rigidbody2D rb;

    protected SpriteRenderer sr;
    
    public int team = 0;

    public Animator anim { get; set; }
    public int leftAnimationHash { get; set; }
    public int rightAnimationHash { get; set; }
    public int upAnimationHash { get; set; }
    public int downAnimationHash { get; set; }

    public Color baseColor;


    // Start is called before the first frame update
    public abstract void Start();


    // Update is called once per frame
    public abstract void Update();

}
