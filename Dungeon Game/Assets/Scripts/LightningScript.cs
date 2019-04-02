using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{

    public Vector3 direction = Vector3.up;
    public Vector3Int pos1;
    public Vector3Int pos2;
    public float speed = 0.02f;
    public int intervals = 3;
    public float intervalLength = .5f / 3;
    public float damage = 3;
    public bool crit = false;
    public HealthEntity caster;
    private float timeElapsed = .5f / 3;
    private int interval = 0;
    // Start is called before the first frame update
    void Start()
    {
        direction.Normalize();
        transform.position = new Vector3(caster.tilePos.x + (direction.x / 5), caster.tilePos.y + ((direction.y == 0)? -0.4f : (direction.y > 0) ? -0.4f : -0.75f ), caster.tilePos.y + 4f);
        transform.rotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.up, direction, Vector3.forward));
        pos1 = new Vector3Int(caster.tilePos.x + ((int)direction.x), caster.tilePos.y + ((int)direction.y), 0);
        pos2 = new Vector3Int(caster.tilePos.x + ((int)direction.x * 2), caster.tilePos.y + ((int)direction.y * 2), 0);
        Transform t = transform.GetChild(0);
        if (!Control.c.IsTile(pos2))
        {
            t.localScale = new Vector3(1, 0.65f, 1);
        }
        if (!Control.c.IsTile(pos1))
        {
            t.localScale = new Vector3(1, 0.15f, 1);
        }
        caster.frozen = true;
    }


    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= intervalLength)
        {
            timeElapsed -= intervalLength;
            Entity e = Control.c.GetEntity(pos1);
            if (Control.c.IsTile(pos1) && e != null && e != caster)
            {
                if (e is HealthEntity)
                {
                    new DamageEvent(e as HealthEntity, caster, damage, "projectile", crit).Invoke();
                }

            }
            e = Control.c.GetEntity(pos2);
            if (Control.c.IsTile(pos1) && e != null && e != caster)
            {
                if (e is HealthEntity)
                {
                    new DamageEvent(e as HealthEntity, caster, damage, "projectile", crit).Invoke();
                }

            }
            interval += 1;
        }
        if (interval >= 4)
        {
            caster.frozen = false;
            Destroy(gameObject);
        }
    }
}
