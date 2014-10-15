using UnityEngine;
using System.Collections;
using System;

public class ExplosionBehaviour : MonoBehaviour {

    private float timer;
    private float explosionForce;
    private int damage;
    void Start()
    {
        SetExplosion();
        SetExplosionMultiplier();
    }
    public void SetExplosion(float explosionForce = 1000.0f, int damage = 1, float radius = 1.2f)
    {
        this.explosionForce = explosionForce;
        this.damage = damage;
        ((SphereCollider)this.collider).radius = radius;
    }
    public void SetExplosionMultiplier(float explosionForceMultiplier = 1.0f, int damageMultiplier = 1, float radiusMulitiplier = 1.0f)
    {
        explosionForce *= explosionForceMultiplier;
        damage *= damageMultiplier;
        ((SphereCollider)this.collider).radius *= radiusMulitiplier;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
        if (timer > particleSystem.duration * 0.25f)
        {
            collider.enabled = false;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        try
        {
            collider.GetComponent<EnemyBehaviour>().Damage(damage);
            collider.GetComponent<EnemyBehaviour>().agent.enabled = false;
            collider.rigidbody.AddExplosionForce(explosionForce, transform.position + new Vector3(0, -1, 0), ((SphereCollider)this.collider).radius);
            collider.rigidbody.useGravity = true;
        }
        catch (Exception) { }
    }

}
