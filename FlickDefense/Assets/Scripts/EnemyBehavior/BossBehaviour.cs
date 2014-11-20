using UnityEngine;
using System.Collections;

public class BossBehaviour : EnemyBehaviour {

    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;

        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        location.GetComponent<PersonalLocationManager>().SetObject(gameObject);
        speed *= 0.5f;
        agent.speed = speed;
    }

    private void ThrowAtScreen()
    {

    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        UserStatus.Instance.DamageCastle();
        timer = 0;
    }
}
