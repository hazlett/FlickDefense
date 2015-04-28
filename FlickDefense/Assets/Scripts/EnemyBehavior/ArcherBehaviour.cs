using UnityEngine;
using System.Collections;

public class ArcherBehaviour : EnemyBehaviour {

	void Start () {
        float z = Random.Range(-5.0f, 5.0f);
        float x = 7 - (0.5f * Mathf.Abs(z));
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.x += x;
        moveLocation.z += z;

        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        location.GetComponent<PersonalLocationManager>().SetObject(gameObject);
        atLocation = false;
        agent.SetDestination(moveLocation);
	}

    protected override void DestroyEnemy()
    {
        UserStatus.Instance.ArcherKilled();

        base.DestroyEnemy();
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        Invoke("FireProjectile", 0.5f);
    }
    public override void AtLocation()
    {
        transform.rotation = Quaternion.Euler(lookAt);
        transform.GetChild(0).Rotate(Vector3.up, 45.0f);
        base.AtLocation();
    }

    void FireProjectile()
    {
        GameObject arrow = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Enemies/Misc/Arrow"));
        arrow.transform.position = this.transform.position + new Vector3(0.0f, 0.75f, 0.0f);
        arrow.transform.forward = -this.transform.forward;
        arrow.GetComponent<Rigidbody>().velocity = -arrow.transform.forward * 20.0f;
    }
}
