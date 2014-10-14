using UnityEngine;
using System.Collections;

public class FlyerBehaviour : EnemyBehaviour {
    public Transform leftWing, rightWing;
    private int initialHealth;
    private bool dying = false;
    void Start()
    {
        health = 2;
        float y = Random.Range(2.0f, 5.0f);
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.x -= 2.0f;
        moveLocation.y += y;

        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        location.GetComponent<PersonalLocationManager>().SetObject(gameObject);
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
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    public override void Damage()
    {
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
        }
   
    }
    void Update()
    {
        if ((!atLocation) && (!dying))
        {
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, speed);
        }
        else if (dying)
        {
            rigidbody.useGravity = true;
        }
    }
    public override void AtLocation()
    {
        atLocation = true;
        Attack();
    }
    protected override void Attack()
    {
        UserStatus.Instance.DamageCastle(attackAmount);
        GameObject.Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.collider.tag)
        {
            case "Ground":
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
