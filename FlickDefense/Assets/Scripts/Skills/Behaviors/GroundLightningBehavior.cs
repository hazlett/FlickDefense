using UnityEngine;
using System.Collections;

public class GroundLightningBehavior : MonoBehaviour
{

    public int lightningDuration;
    public int damage;
    public bool lightningWall;
    public bool thunderStorm;

    private float timer;

    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lightningDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Grunt" || collider.tag == "Archer" || collider.tag == "Bomber" ||
            collider.tag == "Flyer" || collider.tag == "Catapult" || collider.tag == "Boss")
        {
            if (thunderStorm)
            {
                StartCoroutine(DelayedDamage(Random.Range(0.75f, 2.75f), collider));
            }
            else if (lightningWall)
            {
                StartCoroutine(DelayedDamage(Random.Range(0.05f, 0.15f), collider));
            }
            else
            {
                StartCoroutine(DelayedDamage(Random.Range(0.05f, 0.25f), collider));
            }
        }
    }

    private IEnumerator DelayedDamage(float seconds, Collider collider)
    {
        yield return new WaitForSeconds(seconds);
        collider.GetComponent<EnemyBehaviour>().Damage(damage);
    }
}
