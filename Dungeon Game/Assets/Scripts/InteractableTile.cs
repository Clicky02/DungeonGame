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
        tileX = Mathf.RoundToInt(transform.localPosition.x - 0.4f);
        tileY = Mathf.RoundToInt(transform.localPosition.y - 0.4f);
        Control.c.interactableTiles.Add(new Vector3Int(tileX, tileY, 0), this);
    }

    public virtual void Interact(MoveEvent e)
    {

    }
}
