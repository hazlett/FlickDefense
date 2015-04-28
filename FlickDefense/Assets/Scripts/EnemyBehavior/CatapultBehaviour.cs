using UnityEngine;
using System.Collections;
using System;

public class CatapultBehaviour : EnemyBehaviour {
    public BoxCollider deathCollider;
    public GameObject rightHand, leftHand;

    private GameObject currentRock;

    void Start()
    {
        float z = UnityEngine.Random.Range(-5.0f, 5.0f);
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
        attackAmount = 3;
	}

    void Update()
    {
        transform.LookAt(lookAt);
        if (!atLocation)
        {
            try
            {
                weapon.GetComponent<Renderer>().enabled = weaponVisibleRun;
            }
            catch (Exception)
            {

            }
            agent.updateRotation = true;
            agent.SetDestination(moveLocation);
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            try
            {
                weapon.GetComponent<Renderer>().enabled = weaponVisibleAttack;
            }
            catch (Exception)
            {

            }
            agent.updateRotation = false;
            animator.SetFloat("Speed", 0);
        }
        if (animator.applyRootMotion)
        {
            timer += Time.deltaTime;
            if (timer > 4.677)
            {
                animator.applyRootMotion = false;
                timer = 0;
            }
        }
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        //animator.applyRootMotion = true;
        GameObject rock = GameObject.Instantiate(Resources.Load("Prefabs/Rocks/Rock")) as GameObject;
        currentRock = rock;
        rock.GetComponent<RockBehaviour>().Set(deathCollider, rightHand, leftHand, attackAmount);
        rock.transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y - 0.25f, transform.position.z - 0.5f);

        timer = 0;
    }
    public override void AtLocation()
    {
        atLocation = true;
        animator.SetTrigger("AtLocation");
        InvokeRepeating("Attack", 1, 7);
    }

    protected override void DestroyEnemy()
    {

        UserStatus.Instance.CatapultKilled();

        base.DestroyEnemy();
    }
	

}
