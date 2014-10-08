using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {
    protected Vector3 moveLocation;
    protected float speed = 3.5f, timer;
    protected bool atLocation;
    protected float deathHeight = 5.0f;
    public GameObject weapon;
    public NavMeshAgent agent;
    public Animator animator;
	void Start () {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        atLocation = false;
        agent.SetDestination(moveLocation);
	}

    void OnEnable()
    {
        animator.SetBool("InAir", false);
        agent.enabled = true;
    }
    void OnDisable()
    {
        animator.SetBool("InAir", true);
        agent.enabled = false;
    }
	void Update () {
        transform.LookAt(Vector3.forward);
        if (!atLocation)
        {
            try
            {
                weapon.renderer.enabled = false;
            }
            catch(Exception)
            {

            }
            agent.SetDestination(moveLocation);
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else
        {
            try
            {
                weapon.renderer.enabled = true;
            }
            catch (Exception)
            {

            }
            animator.SetFloat("Speed", 0);
        }
    }
    void OnDestroy()
    {
        Debug.Log("Destroying");
        //Remove from gameManager list
    }
    public virtual void Landed(float fallHeight)
    {
        if (fallHeight > deathHeight)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Debug.Log("Killed me: " + gameObject.name);
        GameObject.Destroy(gameObject);
    }
    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Generic Attack");
        UserStatus.Instance.DamageCastle();
        timer = 0;
    }
    public virtual void AtLocation()
    {
        atLocation = true;
        InvokeRepeating("Attack", 1, 3);
    }
    public void OffLocation()
    {
        atLocation = false;
        CancelInvoke("Attack");
    }

    void OnGUI()
    {
        GUILayout.Box("HEALTH: " + UserStatus.Instance.CastleHealth);
    }
}
