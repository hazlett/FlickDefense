using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    public BoxCollider boxCollider;
    private GameObject rightHand, leftHand;
    private bool tapped = false;
    private bool grabbed = false;
    private bool thrown = false;
    private float timer = 0;
    private float grabTimer = 0;
    private int attackAmount = 3;
    void Update()
    {
        grabTimer += Time.deltaTime;

        if (!thrown)
        {
            if ((grabTimer > 5.0) && (!tapped))
            {
                ThrowRock();
            }
            else if ((grabTimer > 1.16) && (!tapped))
            {
                grabbed = true;
            }
            if (tapped)
            {
                timer += Time.deltaTime;
                if (timer > 5)
                {
                    boxCollider.enabled = false;
                    Destroy(gameObject);
                }
            }
            else if (grabbed)
            {
                if (rightHand != null && leftHand != null)
                {
                    transform.position = (rightHand.transform.position + leftHand.transform.position) / 2;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
    public void Set(BoxCollider boxCollider, GameObject rightHand, GameObject leftHand, int attackAmount)
    {
        this.boxCollider = boxCollider;
        this.rightHand = rightHand;
        this.leftHand = leftHand;
        this.attackAmount = attackAmount;
    }
    public void Tap()
    {
        if (tapped)
        {
            return;
        }
        grabbed = false;
        tapped = true;
        rigidbody.useGravity = true;
        if (transform.position.y > (boxCollider.transform.position.y + ((SphereCollider)collider).radius))
        {
            rigidbody.AddForce(0.0f, 250.0f, 0.0f);
        }
        else
        {
            collider.enabled = false;
        }
        boxCollider.enabled = true;
    }
    private void ThrowRock()
    {
        grabbed = false;
        thrown = true;
        rigidbody.angularDrag = 0;
        rigidbody.useGravity = true;
        rigidbody.AddForce(-1000.0f, 100.0f, 10.0f);
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
                    GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/Particles/RockHit")) as GameObject;
                    go.transform.position = transform.position;
                    UserStatus.Instance.DamageCastle(3);
                    Destroy(gameObject);
                }
                break;
            case "DeathCollider":
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
