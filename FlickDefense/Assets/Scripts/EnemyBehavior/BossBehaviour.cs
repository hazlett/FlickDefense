using UnityEngine;
using System.Collections;

public class BossBehaviour : EnemyBehaviour {

    void Start()
    {
        moveLocation = GameObject.Find("CastleDoor").transform.position;

        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        speed *= 0.5f;
        agent.speed = speed;
    }

    protected override void Attack()
    {
        GameStateManager.Instance.DamageCastle(5);
    }
}
