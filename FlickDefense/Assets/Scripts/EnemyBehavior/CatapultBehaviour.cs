using UnityEngine;
using System.Collections;

public class CatapultBehaviour : EnemyBehaviour {
    public BoxCollider deathCollider;
    public GameObject leftHand, rightHand;
    void Start()
    {
        float z = Random.Range(-5.0f, 5.0f);
        float x = 10 - (0.5f * Mathf.Abs(z));
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.x += x;
        moveLocation.z += z;

        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        location.GetComponent<PersonalLocationManager>().SetObject(gameObject);
        speed *= 0.5f;
        agent.speed = speed;
        atLocation = false;
        agent.SetDestination(moveLocation);
	}
    public override void Damage()
    {
        Die();
    }
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        GameObject rock = GameObject.Instantiate(Resources.Load("Prefabs/Rocks/Rock")) as GameObject;
        rock.GetComponent<RockBehaviour>().Set(deathCollider, rightHand, leftHand);
        rock.transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y - 0.25f, transform.position.z - 0.5f);
        UserStatus.Instance.DamageCastle(3);
    }
    public override void AtLocation()
    {
        atLocation = true;
        animator.SetTrigger("AtLocation");
        InvokeRepeating("Attack", 7, 7);
    }
	

}
