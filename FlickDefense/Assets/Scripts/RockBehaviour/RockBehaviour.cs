using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    public BoxCollider boxCollider;
    private GameObject rockPosition;
    private bool tapped = false;
    private bool grabbed = false;
    private bool thrown = false;
    private float timer = 0;
    void Update()
    {
        if (!thrown)
        {
            if (tapped)
            {
                timer += Time.deltaTime;
                if (timer > 10)
                {
                    Destroy(gameObject);
                }
            }
            else if (grabbed)
            {
                transform.position = rockPosition.transform.position;
            }
        }
    }
    public void Set(BoxCollider boxCollider, GameObject rockPosition)
    {
        this.boxCollider = boxCollider;
        this.rockPosition = rockPosition;
    }
    public void Tap()
    {
        grabbed = false;
        tapped = true;
        boxCollider.enabled = true;
        gameObject.layer = 8;
    }
    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "CatapultDeath":
                {
                    collider.GetComponent<CatapultDeath>().Catapult.GetComponent<EnemyBehaviour>().Damage();
                }
                break;
            case "Wall":
                {

                }
                break;
            case "Hand":
                {
                    grabbed = true;
                    ((SphereCollider)this.collider).radius = 0.5f;
                }
                break;
            case "ThrowRock":
                {
                    grabbed = false;
                    thrown = true;
                    rigidbody.angularDrag = 0;
                    rigidbody.useGravity = true;
                    rigidbody.AddForce(-1000.0f, 100.0f, 10.0f);
                }
                break;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        switch (collider.tag)
        {
            case "ThrowRock":
                {
                    this.collider.isTrigger = false;
                }
                break;
        }
    }
}
