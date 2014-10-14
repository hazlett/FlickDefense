using UnityEngine;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour {
    protected Vector3 moveLocation;
    protected float speed = 3.5f, timer;
    protected bool atLocation;
    protected float damageHeight = 5.0f;
    protected Vector3 lookAt = Vector3.forward;
    protected bool weaponVisibleRun = false;
    protected bool weaponVisibleAttack = true;
    public GameObject weapon;
    public NavMeshAgent agent;
    public Animator animator;
    protected int health = 1;
    protected int level = 0;
    protected int attackAmount = 1;
	void Start () {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        atLocation = false;
        agent.SetDestination(moveLocation);
	}
    protected virtual void SetStats(float speed, int health, float damageHeight, int attackAmount)
    {
        this.speed = speed;
        this.health = health;
        this.damageHeight = damageHeight;
    }
    public virtual void SetLevel(int level)
    {
        this.level = level;
        SetStats(speed * (1 + (level / 10)), health * (1 + level), damageHeight * (1 + (level / 10)), attackAmount * (1 + (level / 5)));
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
        if (fallHeight > damageHeight)
        {
            Damage((int)(fallHeight/damageHeight));
        }
    }
    public virtual void Damage()
    {
        Damage(1);
    }
    public virtual void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
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
        UserStatus.Instance.DamageCastle(attackAmount);
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
