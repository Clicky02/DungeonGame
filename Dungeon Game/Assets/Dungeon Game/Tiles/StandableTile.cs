using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]

public class StandableTile : Tile
{
    public Dictionary<Vector3Int, HealthEntity> entities = new Dictionary<Vector3Int, HealthEntity>();

}
