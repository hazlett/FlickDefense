using UnityEngine;
using System.Collections;

public class FlyerBehaviour : EnemyBehaviour {
    void Start()
    {
        float y = Random.Range(2.0f, 5.0f);
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation.x -= 2.0f;
        moveLocation.y += y;

        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        location.GetComponent<PersonalLocationManager>().SetObject(gameObject);
        speed = 0.05f;
        atLocation = false;
    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    void Update()
    {
        if (!atLocation)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, speed);
        }
    }
    public override void AtLocation()
    {
        atLocation = true;
        Attack();
    }
    protected override void Attack()
    {
        Debug.Log("Bomber Suicide");
        GameStateManager.Instance.DamageCastle(2);
        GameObject.Destroy(gameObject);
    }

}
