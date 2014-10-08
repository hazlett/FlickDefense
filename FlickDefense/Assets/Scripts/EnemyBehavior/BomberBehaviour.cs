using UnityEngine;
using System.Collections;

public class BomberBehaviour : EnemyBehaviour {
    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.z += Random.Range(-10, 10);
        atLocation = false;
        agent.SetDestination(moveLocation);
        speed *= 5.0f;
        agent.speed = speed;;
    }

    protected override void Attack()
    {
        Debug.Log("Bomber Suicide");
        UserStatus.Instance.DamageCastle(2);
        GameObject.Destroy(gameObject);
    }
    public override void AtLocation()
    {
        atLocation = true;
        Attack();
    }
}
