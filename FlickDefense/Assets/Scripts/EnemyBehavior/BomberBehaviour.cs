using UnityEngine;
using System.Collections;

public class BomberBehaviour : EnemyBehaviour {

    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.z += Random.Range(-6, 6);
        atLocation = false;
        speed *= 2.5f;
        agent.speed = speed;
        agent.SetDestination(moveLocation);
        weaponVisibleRun = true;
    }

    protected override void Die()
    {
        base.Die();

        Explode();
    }

    protected override void DestroyEnemy()
    {
        UserStatus.Instance.BomberKilled();
        base.DestroyEnemy();
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
        Explode();
    }

}
