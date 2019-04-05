using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Control : MonoBehaviour
{
    public static Control c;

    public Player player;
    public Tilemap tilemap;
    public Tilemap layerTwo;
    public Tilemap layerThree;
    public Dictionary<Vector3Int, List<Projectile>> projectiles = new Dictionary<Vector3Int, List<Projectile>>();
    public Dictionary<Vector3Int, Entity> entities = new Dictionary<Vector3Int, Entity>();
    public Dictionary<Vector3Int, InteractableTile> interactableTiles = new Dictionary<Vector3Int, InteractableTile>();
    private Vector2 tOrigin = -Vector2.one;


    /*
     * These are how the buttons interact with this script.
     * AbilityActive is set to true to when the button is held down
     * and the player should be rotating not moving.
     * Nextability is set to the abilityid when the button is released
     * so that the script knows to cast it.
     */
    public bool abilityActive = false;
    public int nextAbility = -1;

    public int[] nextMovement = null;
    bool swiped = false;
    List<int> inelligbleTouches = new List<int>();
    int activeTouch = -999; //Touch is nonnullable, and this seems to be the obvious way around it

    private void Awake()
    {
        c = this;
    }

    private void Start()
    {

        nextMovement = null;
        nextAbility = -1;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(10, 10);

        vector.Normalize();

        int x = 0;
        int y = 0;
        int ability = -2;

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR


        if (Input.GetKey(KeyCode.Alpha1))
        {
            ability = -1;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ability = 0;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            y = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            y = 1;
        }

        if (player.movement != null)
        {
            x = 0;
            y = 0;
        }


#else

        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = myTouches[i];
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > (Screen.width - 300) && t.position.y  < 300)
                {
                    inelligbleTouches.Add(t.fingerId);
                }
                else if (activeTouch == -999)
                {
                    tOrigin = t.position;
                    activeTouch = t.fingerId;
                }
            }
            else if (inelligbleTouches.Contains(t.fingerId))
            {
                if (t.phase == TouchPhase.Ended)
                {
                    inelligbleTouches.Remove(t.fingerId);
                }
            }
            else if (t.fingerId == activeTouch)
            {
                Vector2 tEnd = t.position;
                float tx = tEnd.x - tOrigin.x;
                float ty = tEnd.y - tOrigin.y;
                float ax = Mathf.Abs(tx);
                float ay = Mathf.Abs(ty);



                if (t.phase == TouchPhase.Ended && tOrigin.x >= 0)
                {
                    tOrigin.x = -1;
                    activeTouch = -999;


                    if (ax < 18 && ay < 18)
                    {
                        x = (int)player.direction.x;
                        y = (int)player.direction.y;
                    }
                    else
                    {
                        if (ax > ay)
                        {
                            x = tx > 0 ? 1 : -1;
                        }
                        else
                        {
                            y = ty > 0 ? 1 : -1;
                        }
                    }
                    if (player.movement != null && (x != 0 || y != 0))
                    {
                        if (!swiped) nextMovement = new int[2] { x, y };
                        x = 0;
                        y = 0;
                    }
                    swiped = false;
                }
                else if ((ax > 120 || ay > 120) && player.movement == null)
                {
                    swiped = true;
                    if (ax > ay)
                    {
                        x = tx > 0 ? 1 : -1;
                    }
                    else
                    {
                        y = ty > 0 ? 1 : -1;
                    }
                }

            }
        }
        



        if (player.movement == null && nextMovement != null && (x == 0 && y == 0))
        {
            x = nextMovement[0];
            y = nextMovement[1];
            nextMovement = null;
        }



#endif

        if (nextAbility != -1)
        {
            ability = nextAbility;
            nextAbility = -1;
        }

        if (abilityActive == true)
        {
            ability = -1;
        }


        if (ability == -2)
        {

            if (x != 0 || y != 0)
            {
                player.Move(new Vector3(x, y, 0));
            }

        }
        else if (ability == -1)
        {
            if (x != 0 || y != 0)
                player.Rotate(new Vector3(x, y, 0));
        }
        else
        {
            player.UseAbility(ability);
        }
    }

    public InteractableTile GetTrap(Vector3Int pos)
    {
        return interactableTiles.ContainsKey(pos) ? interactableTiles[pos] : null;
    }

    public bool IsTile(Vector3Int pos)
    {
        return tilemap.GetTile(pos) != null;
    }

    public void SetEntity(Vector3Int pos, Entity e)
    {
        entities.Add(pos, e);
    }

    public void RemoveEntity(Vector3Int pos)
    {
        entities.Remove(pos);
    }

    public Entity GetEntity(Vector3Int pos)
    {
        if (entities.ContainsKey(pos))
        {
            return entities[pos];
        }
        return null;
    }

    public void AddProjectile(Vector3Int pos, Projectile e)
    {
        if (!projectiles.ContainsKey(pos))
        {
            projectiles.Add(pos, new List<Projectile>());

        }
        projectiles[pos].Add(e);
    }

    public void RemoveProjectile(Vector3Int pos, Projectile e)
    {
        projectiles[pos].Remove(e);
        if (projectiles[pos].Count < 1)
        {
            projectiles.Remove(pos);
        }

    }

    public List<Projectile> GetProjectiles(Vector3Int pos)
    {
        return projectiles[pos];
    }
}
