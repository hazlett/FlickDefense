using UnityEngine;
using System.Collections;
using System;

public class ExplosionBehaviour : MonoBehaviour {

    public SphereCollider explosionCollider;

    private float timer;
    private float explosionForce = 500f;
    private int damage = 1;
    private bool damageWall = true;

    public void SetExplosion(float explosionForce = 1000.0f, int damage = 1, float radius = 1.0f)
    {
        this.explosionForce = explosionForce;
        this.damage = damage;
        explosionCollider.radius = radius;
        damageWall = false;
    }
    public void SetExplosionMultiplier(float explosionForceMultiplier = 1.0f, int damageMultiplier = 1, float radiusMulitiplier = 1.0f)
    {
        explosionForce *= explosionForceMultiplier;
        damage *= damageMultiplier;
        explosionCollider.radius *= radiusMulitiplier;
        damageWall = false;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (!GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }
        if (timer > GetComponent<ParticleSystem>().duration * 0.25f)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            UserStatus.Instance.DamageCastle();
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "Wall") && damageWall)
        {
            UserStatus.Instance.DamageCastle();
        }
        else
        {
            try
            {
                collider.GetComponent<EnemyBehaviour>().Damage(damage);
                collider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position + new Vector3(0, -1, 0), ((SphereCollider)this.GetComponent<Collider>()).radius);
                collider.GetComponent<Rigidbody>().useGravity = true;
            }
            catch (Exception) { }
        }
    }

}
