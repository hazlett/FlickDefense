using UnityEngine;
using System.Collections;

public class FireballBehavior : MonoBehaviour {

    internal Vector3 direction;
    internal bool level1;
	
	void Update () {
        MoveFireball();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
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
        GameObject explosion;
        if (level1)
        {
            explosion = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/BomberExplosion"));
        }
        else
        {
            explosion = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/BomberExplosion"));
        }
        explosion.transform.position = this.transform.position + new Vector3(0.0f, 0.5f, 0.0f);

        GameObject.Destroy(this.gameObject);
    }
}
