using UnityEngine;
using System.Collections;

public class LightningStrikeSpawner : MonoBehaviour {

    private static LightningStrikeSpawner instance;
    public static LightningStrikeSpawner Instance { get { return instance; } set { instance = value; } }

    public float cooldownPeriod;

    private Ray ray;
    private RaycastHit hit;

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

    internal void LaunchFireball(Vector2 touchPosition, bool level1)
    {
        ray = Camera.main.ScreenPointToRay(touchPosition);
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        GameObject lightningBolt = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Lightning/LightningBolt"));
        
        if (!level1)
        {
            lightningBolt.transform.GetChild(0).GetComponent<ParticleSystem>().startSize *= 3.0f;
            SkillHandler.Instance.cooldownPeriod = cooldownPeriod + 10;
        }
        else
        {
            SkillHandler.Instance.cooldownPeriod = cooldownPeriod;
        }
    }
}
