using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public int tileX;
    public int tileY;
    public float elapsedTime = 0.0f;
    public int state = 0;
    private SpriteRenderer sr;
    public ResourceHolder res;
    public HealthEntity damaged = null;

    // Start is called before the first frame update
    void Start()
    {
        tileX = (int) (transform.position.x);
        tileY = (int) (transform.position.y - 0.4f);
        sr = GetComponent<SpriteRenderer>();
        res = ResourceHolder.r;

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1f)
        {
            if (state == 0)
            {
                state = 1;
                sr.sprite = res.spikeTrap[0];
                damaged = null;
            }
            else if (state == 1)
            {
                state = 2;
                sr.sprite = res.spikeTrap[1];
            }
            else if (state == 2)
            {
                state = 0;
                sr.sprite = res.spikeTrap[2];
                HealthEntity e = res.c.GetEntity(new Vector3Int(tileX, tileY, 0)) as HealthEntity;
                if (e != null && e != damaged)
                {
                    new DamageEvent(e, null, 4.0f, "trap", false).Invoke();
                    damaged = e;
                }
            }
            elapsedTime -= 1f;
        }
        if (state == 0 )
        {
            HealthEntity e = res.c.GetEntity(new Vector3Int(tileX, tileY, 0)) as HealthEntity;
            if (e != null && e != damaged)
            {
                new DamageEvent(e, null, 4.0f, "trap", false).Invoke();
                damaged = e;
            }
        }
    }
}
