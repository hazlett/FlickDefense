using UnityEngine;
using System.Collections;

public class FirewallBehavior : MonoBehaviour {

    public int fireDuration;

    private float timer;

	// Use this for initialization
    void Start()
    {
        timer = 0.0f;
    }
	
	// Update is called once per frame
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
            collider.GetComponent<EnemyBehaviour>().Damage();

            GameObject enemyFire = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/EnemyFire"));

            enemyFire.transform.position = collider.transform.position;
            enemyFire.transform.parent = collider.transform;
        }
    }
}
