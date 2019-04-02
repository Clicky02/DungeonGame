using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Entity : MonoBehaviour
{ 
    public Control c;
    public Vector3Int tilePos = new Vector3Int(0, 0, 0);

    protected SpriteRenderer sr;

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
