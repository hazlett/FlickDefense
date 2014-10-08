using UnityEngine;
using System.Collections;

public class BomberBehaviour : EnemyBehaviour {
    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.z += Random.Range(-10, 10);
        atLocation = false;
        speed *= 5.0f;
        agent.speed = speed;
        agent.SetDestination(moveLocation);
    }

    protected override void Attack()
    {
        Debug.Log("Bomber Suicide");
        GameStateManager.Instance.DamageCastle(2);
        GameObject.Destroy(gameObject);
    }
    public override void AtLocation()
    {
        atLocation = true;
        Attack();
    }
}
