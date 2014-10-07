using UnityEngine;
using System.Collections;

public class CatapultBehaviour : EnemyBehaviour {

	void Start () {
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

    protected override void Attack()
    {
        Debug.Log("Catapult Launch");
        GameStateManager.Instance.DamageCastle(3);
    }
    public override void AtLocation()
    {
        atLocation = true;
        InvokeRepeating("Attack", 7, 7);
    }
	

}
