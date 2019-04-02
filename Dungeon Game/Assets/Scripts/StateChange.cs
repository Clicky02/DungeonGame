using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateChange
{
    protected Entity e;
    public abstract bool Tick(float delta);
}
