using UnityEngine;
using System.Collections;

public class GroundLightningBehavior : MonoBehaviour
{

    public int lightningDuration;
    public int damage;
    public bool lightningWall;

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
            collider.GetComponent<EnemyBehaviour>().Damage(damage);
        }
    }
}
