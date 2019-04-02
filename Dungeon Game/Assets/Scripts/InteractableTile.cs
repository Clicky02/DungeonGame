using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTile : MonoBehaviour
{

    public int tileX;
    public int tileY;

    // Start is called before the first frame update
    public virtual void Start()
    {
        tileX = (int)(transform.position.x);
        tileY = (int)(transform.position.y - 0.4f);
        Control.c.interactableTiles.Add(new Vector3Int(tileX, tileY, 0), this);
    }

    public virtual void Interact(MoveEvent e)
    {

    }
}
