using UnityEngine;
using System.Collections;

public class GruntBehaviour : EnemyBehaviour {

    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.z += Random.Range(-5, 5);
        atLocation = false;
        agent.SetDestination(moveLocation);
        Debug.Log("Started Grunt");
    }

    protected override void Attack()
    {
        base.Attack();
    }

}
