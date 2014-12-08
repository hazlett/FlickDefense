using UnityEngine;
using System.Collections;

public class FireballBehavior : MonoBehaviour {

    internal Vector3 direction;
    internal bool level1 = true, rainOfFire = false;
	
	void Update () {
        MoveFireball();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Fireball")
        {
            ExplodeFireball();
        }
    }

    private void MoveFireball()
    {
        this.rigidbody.AddForce(direction * 5f);
    }

    private void ExplodeFireball()
    {
        GameObject explosion = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/BomberExplosion"));
            explosion.GetComponent<ExplosionBehaviour>().SetExplosionMultiplier(1.0f, 3, 1.0f);
        if (!level1)
        {
            explosion.GetComponent<ExplosionBehaviour>().SetExplosionMultiplier(1.0f, 3, 1.5f);
        }
        if (rainOfFire)
        {
            explosion.GetComponent<ExplosionBehaviour>().SetExplosionMultiplier(1.0f, 5, 2.0f);
        }
        explosion.transform.position = this.transform.position + new Vector3(0.0f, 0.5f, 0.0f);

        GameObject.Destroy(this.gameObject);
    }
}
