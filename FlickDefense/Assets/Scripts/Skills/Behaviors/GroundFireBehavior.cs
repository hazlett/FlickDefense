using UnityEngine;
using System.Collections;

public class GroundFireBehavior : MonoBehaviour {

    public int fireDuration;
    public int damage;
    public bool firewall;

    private float timer;

    void Start()
    {
        timer = 0.0f;
    }
	
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > fireDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Grunt" || collider.tag == "Archer" || collider.tag == "Bomber" || 
            collider.tag == "Flyer" || collider.tag == "Catapult" || collider.tag == "Boss")
        {
            collider.GetComponent<EnemyBehaviour>().Damage(damage);

            GameObject enemyFire = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/EnemyFire"));

            if (!firewall)
            {
                enemyFire.GetComponent<DamageEnemyOverTime>().damage = 3;
            }

            enemyFire.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y + 0.5f, collider.transform.position.z);
            enemyFire.transform.parent = collider.transform;
        }
    }
}
