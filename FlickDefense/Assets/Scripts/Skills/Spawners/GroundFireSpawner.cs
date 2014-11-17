using UnityEngine;
using System.Collections;

public class GroundFireSpawner : MonoBehaviour {

    private static GroundFireSpawner instance;
    public static GroundFireSpawner Instance { get { return instance; } set { instance = value; } }

    public float stormCooldown, wallCooldown;

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

    internal void CreateFirewall(Vector2 startPosition, Vector2 endPosition )
    {
        rayBegin = Camera.main.ScreenPointToRay(startPosition);
        rayEnd = Camera.main.ScreenPointToRay(endPosition);

        Physics.Raycast(rayBegin, out hitBegin, Mathf.Infinity);
        Physics.Raycast(rayEnd, out hitEnd, Mathf.Infinity);

        GameObject firewall = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/Firewall"));

        firewall.transform.position = (hitBegin.point + hitEnd.point) / 2;
        firewall.transform.LookAt(hitBegin.point);

        SkillHandler.Instance.cooldownPeriod = wallCooldown;
    }


    internal void CreateFireStorm(Vector2 touchPosition)
    {
        rayBegin = Camera.main.ScreenPointToRay(touchPosition);
        Physics.Raycast(rayBegin, out hitBegin, Mathf.Infinity);

        GameObject firestorm = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/Firestorm"));

        firestorm.transform.position = hitBegin.point;

        SkillHandler.Instance.cooldownPeriod = stormCooldown;
    }

}
