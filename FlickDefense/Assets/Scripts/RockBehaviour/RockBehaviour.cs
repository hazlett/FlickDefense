using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    public BoxCollider boxCollider;
    private GameObject rightHand, leftHand;
    private bool tapped = false;
    private bool grabbed = false;
    private float timer = 0;
    void Update()
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
            transform.position = leftHand.transform.position;
        }
    }
    public void Set(BoxCollider boxCollider, GameObject rightHand, GameObject leftHand)
    {
        this.boxCollider = boxCollider;
        this.rightHand = rightHand;
        this.leftHand = leftHand;
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
                }
                break;
        }
    }
}
