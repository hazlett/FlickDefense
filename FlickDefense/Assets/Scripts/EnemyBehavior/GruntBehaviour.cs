using UnityEngine;
using System.Collections;

public class GruntBehaviour : EnemyBehaviour {

    private bool slashParticle;
    private GameObject slashAttack;

    void Start()
    {
        GameObject castle = GameObject.Find("GruntLocation");
        moveLocation = castle.transform.position;
        float rangeX = castle.GetComponent<Collider>().bounds.extents.z;
        float rangeZ = GetComponent<CapsuleCollider>().bounds.extents.x;
        moveLocation.z += Random.Range(-1f * rangeX, rangeX);
        moveLocation.x += Random.Range(-1f * rangeZ, rangeZ);
        atLocation = false;
        agent.SetDestination(moveLocation);
        slashAttack = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/GruntSwing"));
        slashAttack.transform.position = this.transform.position + new Vector3(-0.5f, 1, 0);
        slashAttack.transform.parent = this.gameObject.transform;
        slashAttack.SetActive(false);
        
    }

    protected override void DestroyEnemy()
    {
        GameObject splatter = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/GruntSplat"));
        splatter.transform.position = this.transform.position;

        dead = true;
        UserStatus.Instance.GruntKilled();

        base.DestroyEnemy();
    }

    protected override void Attack()
    {
        base.Attack();

        if (!slashParticle)
        {
            slashAttack.transform.position = this.transform.position + new Vector3(-0.5f, 1, 0);
            slashParticle = true;
        }
    }

    public override void AtLocation()
    {
        base.AtLocation();
        slashAttack.SetActive(true);
    }

    public override void OffLocation()
    {
        base.OffLocation();
        slashAttack.SetActive(false);
    }

}
