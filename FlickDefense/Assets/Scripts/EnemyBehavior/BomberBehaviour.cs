using UnityEngine;
using System.Collections;

public class BomberBehaviour : EnemyBehaviour {
    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.z += Random.Range(-10, 10);
        atLocation = false;
        speed *= 2.5f;
        agent.speed = speed;
        agent.SetDestination(moveLocation);
        weaponVisibleRun = true;
    }

    protected override void Attack()
    {
        UserStatus.Instance.DamageCastle(2);
        GameObject.Destroy(gameObject);
    }
    public override void AtLocation()
    {
        atLocation = true;
        Attack();
    }
}
