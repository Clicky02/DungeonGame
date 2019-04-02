using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    public static ResourceHolder r;

    public List<Sprite> spikeTrap = new List<Sprite>();
    public GameObject damageNumber;
    public Control c;
    // Start is called before the first frame update

    private void Awake()
    {
        r = this;
    }

    void Start()
    {
        

        c = Control.c;
        spikeTrap.Add(Resources.Load("floor_spikes_anim_f0", typeof(Sprite)) as Sprite);
        spikeTrap.Add(Resources.Load("floor_spikes_anim_f1", typeof(Sprite)) as Sprite);
        spikeTrap.Add(Resources.Load("floor_spikes_anim_f3", typeof(Sprite)) as Sprite);
    }
}
