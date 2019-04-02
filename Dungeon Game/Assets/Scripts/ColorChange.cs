using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : StateChange
{
    SpriteRenderer sr;
    Color origColor;
    float dur = 1f;
    float timeLapsed = 0f;

    public ColorChange(Entity entity, Color color, float duration, SpriteRenderer sr1)
    {
        e = entity;
        sr = sr1;
        origColor = sr.color;
        sr.color = color;
        dur = duration;
    }

    public override bool Tick(float delta)
    {
        timeLapsed += delta;
        if (timeLapsed > dur)
        {
            sr.color = e.baseColor;
            return true;
        }
        return false;
    }
}
