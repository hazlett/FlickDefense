﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {
    protected Vector3 moveLocation;
    protected float speed = 3.5f, timer;
    protected bool atLocation;
    protected float deathHeight = 5.0f;
    protected Vector3 lookAt = Vector3.forward;
    protected bool weaponVisibleRun = false;
    protected bool weaponVisibleAttack = true;
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
        transform.LookAt(lookAt);
        if (!atLocation)
        {
            try
            {
                weapon.renderer.enabled = weaponVisibleRun;
            }
            catch(Exception)
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
                weapon.renderer.enabled = weaponVisibleAttack;
            }
            catch (Exception)
            {

            }
            agent.updateRotation = false;
            animator.SetFloat("Speed", 0);
        }
    }
    void OnDestroy()
    {
        GameStateManager.Instance.enemyCount--;
    }
    public virtual void Landed(float fallHeight)
    {
        if (fallHeight > deathHeight)
        {
            Die();
        }
    }
    public virtual void Damage()
    {

    }
    protected virtual void Die()
    {
        animator.SetTrigger("Kill");
        //StartCoroutine("DeathAnimation");
        GameObject.Destroy(this.gameObject);
    }
    protected virtual IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(animator.animation.clip.length);
    }
    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
        UserStatus.Instance.DamageCastle();
        timer = 0;
    }
    public virtual void AtLocation()
    {
        animator.SetTrigger("AtLocation");
        atLocation = true;
        InvokeRepeating("Attack", 1, 3);
    }
    public virtual void OffLocation()
    {
        atLocation = false;
        CancelInvoke("Attack");
    }

}
