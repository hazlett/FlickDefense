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
        FireProjectile();
    }
    public override void AtLocation()
    {
        transform.rotation = Quaternion.Euler(lookAt);
        transform.GetChild(0).Rotate(Vector3.up, 45.0f);
        base.AtLocation();
    }

    void FireProjectile()
    {
        Debug.Log("Firing arrow");
    }
}
