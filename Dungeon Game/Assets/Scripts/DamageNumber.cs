using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    private float duration = 1f;
    private float distanceY = .05f;
    public TextMesh t;
    public int damage = 0;
    public Color c = new Color(1, 1, 1, 1);

    private void Start()
    {
        t.text = "" + damage;
        GetComponent<Renderer>().sortingOrder = 100;
        t.color = new Color(c.r, c.g, c.b, duration);
    }

    // Update is called once per frame

    void Update()
    {
        duration -= Time.deltaTime;

        if (duration < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (distanceY * duration), duration);
            t.color = new Color(c.r, c.g, c.b, duration);
        }

       
        
    }
}
