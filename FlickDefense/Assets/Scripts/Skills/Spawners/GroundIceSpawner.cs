using UnityEngine;
using System.Collections;

public class GroundIceSpawner : MonoBehaviour {

    private static GroundIceSpawner instance;
    public static GroundIceSpawner Instance { get { return instance; } set { instance = value; } }

    public float blizzardCooldown, snowStormCooldown, wallCooldown;

    private Ray rayBegin, rayEnd;
    private RaycastHit hitBegin, hitEnd;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    internal void CreateIceWall(Vector2 startPosition, Vector2 endPosition )
    {
        rayBegin = Camera.main.ScreenPointToRay(startPosition);
        rayEnd = Camera.main.ScreenPointToRay(endPosition);

        Physics.Raycast(rayBegin, out hitBegin, Mathf.Infinity);
        Physics.Raycast(rayEnd, out hitEnd, Mathf.Infinity);

        GameObject iceWall = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Ice/IceWall"));

        iceWall.transform.position = (hitBegin.point + hitEnd.point) / 2;
        iceWall.transform.LookAt(hitBegin.point);

        SkillHandler.Instance.cooldownPeriod = wallCooldown;
    }


    internal void CreateSnowStorm(Vector2 touchPosition)
    {
        rayBegin = Camera.main.ScreenPointToRay(touchPosition);
        Physics.Raycast(rayBegin, out hitBegin, Mathf.Infinity);

        GameObject SnowStorm = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Ice/SnowStorm"));

        SnowStorm.transform.position = hitBegin.point;

        SkillHandler.Instance.cooldownPeriod = snowStormCooldown;
    }

    internal void CreateBlizzard()
    {
        GameObject blizzard = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Ice/Blizzard"));

        SkillHandler.Instance.cooldownPeriod = blizzardCooldown;
    }

}
