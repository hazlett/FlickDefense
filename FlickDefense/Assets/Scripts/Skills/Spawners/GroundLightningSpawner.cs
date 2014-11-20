using UnityEngine;
using System.Collections;

public class GroundLightningSpawner : MonoBehaviour {

    private static GroundLightningSpawner instance;
    public static GroundLightningSpawner Instance { get { return instance; } set { instance = value; } }

    public float thunderstormCooldown, lightningstormCooldown, wallCooldown;

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

    internal void CreateLightningwall(Vector2 startPosition, Vector2 endPosition )
    {
        rayBegin = Camera.main.ScreenPointToRay(startPosition);
        rayEnd = Camera.main.ScreenPointToRay(endPosition);

        Physics.Raycast(rayBegin, out hitBegin, Mathf.Infinity);
        Physics.Raycast(rayEnd, out hitEnd, Mathf.Infinity);

        GameObject lightningWall = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Lightning/LightningWall"));

        lightningWall.transform.position = (hitBegin.point + hitEnd.point) / 2;
        lightningWall.transform.LookAt(hitBegin.point);

        SkillHandler.Instance.cooldownPeriod = wallCooldown;
    }


    internal void CreateLightningStorm(Vector2 touchPosition)
    {
        rayBegin = Camera.main.ScreenPointToRay(touchPosition);
        Physics.Raycast(rayBegin, out hitBegin, Mathf.Infinity);

        GameObject LightningStorm = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Lightning/LightningStorm"));

        LightningStorm.transform.position = hitBegin.point;

        SkillHandler.Instance.cooldownPeriod = lightningstormCooldown;
    }

    internal void CreateThunderStorm()
    {
        GameObject LightningStorm = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Lightning/ThunderStorm"));

        SkillHandler.Instance.cooldownPeriod = thunderstormCooldown;
    }

}
