using UnityEngine;
using System.Collections;

public class FlyerBehaviour : EnemyBehaviour {
    public Transform leftWing, rightWing;
    private int initialHealth;
    private bool dying = false;
    private bool grounded = false;
    private FlyerLocation flyerLocation;
    void Start()
    {
        health = 2;
        
        GameObject locations = GameObject.Find("FlyerLocations");
        flyerLocation = locations.GetComponent<FlyerLocationsManager>().AssignLocation(gameObject);
        lookAt = locations.transform.position;
        if (flyerLocation == null)
        {
            Destroy(gameObject);
        }
        moveLocation = flyerLocation.gameObject.transform.position;
        speed = 0.05f;
        atLocation = false;
        attackAmount = 2;
        initialHealth = health;
    }
    protected override void SetStats(float speed, int health, float damageHeight, int attackAmount)
    {
        base.SetStats(speed, health, damageHeight, attackAmount);
        initialHealth = this.health;
    }
    void OnDestroy()
    {
        GameStateManager.Instance.enemyCount--;
        UserStatus.Instance.FlyerKilled();
        if (flyerLocation != null)
        {
            flyerLocation.UnAssign();
        }
    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    public override void Damage()
    {
        animator.SetTrigger("Hurt");
        health--;
        if (health == initialHealth / 2)
        {
            Vector3 wingPosition = leftWing.position;
            wingPosition.x += 0.5f;
            wingPosition.y -= 0.4f;
            speed *= 0.5f;
        }
        else if (health <= 0)
        {
            Vector3 wingPosition = rightWing.position;
            wingPosition.x -= 0.5f;
            wingPosition.y -= 0.4f;
            speed *= 0.0f;
            dying = true;
            animator.SetTrigger("Dying");
            if (grounded)
            {
                Die();
            }
        }
   
    }
    protected override void Die()
    {
        Explode();
        Destroy(gameObject);
    }
    void Update()
    {
        if (dying)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        else if ((!atLocation) && (!dying))
        {
            transform.LookAt(moveLocation);
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, speed);
        }
        else if ((atLocation) && (!dying))
        {
            transform.LookAt(lookAt);
            //GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;        
        }
    }
    public override void AtLocation()
    {
        InvokeRepeating("Attack", 1, 5);
        atLocation = true;
    }
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        GameObject fireBreath = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/FireBreath"));
        fireBreath.transform.position = transform.position;
        UserStatus.Instance.DamageCastle(attackAmount);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (dying)
        {
            Die();
        }
        else
        {
            switch (collision.collider.tag)
            {
                case "Wall":
                    {
                        grounded = true;
                        animator.SetBool("Grounded", true);
                        animator.SetTrigger("Landed");
                        InvokeRepeating("Attack", 1, 5);
                    }
                    break;
            }
        }
    }
}
