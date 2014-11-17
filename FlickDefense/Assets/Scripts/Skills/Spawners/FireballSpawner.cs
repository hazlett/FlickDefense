using UnityEngine;
using System.Collections;

public class FireballSpawner : MonoBehaviour {

    private static FireballSpawner instance;
    public static FireballSpawner Instance { get { return instance; } set { instance = value; } }

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

        GameObject fireball = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/Fireball"));

        Vector3 direction = (fireball.transform.position - hit.point).normalized;
        fireball.GetComponent<FireballBehavior>().direction = -direction;
        fireball.GetComponent<FireballBehavior>().level1 = level1;

        if (!level1)
        {
            fireball.transform.GetChild(0).GetComponent<ParticleSystem>().startSize *= 3.0f;
        }

        SkillHandler.Instance.cooldownPeriod = cooldownPeriod;
    }
}
