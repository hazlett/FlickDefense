using UnityEngine;
using System.Collections;

public class RainOfFireBehavior : MonoBehaviour {

    public int skillDuration, fireballsPerSecond;

    private float timer = 0.0f;

	void Start () {

        float repeatRate = 1.0f / fireballsPerSecond;

        InvokeRepeating("SpawnFireball", 0.01f, repeatRate);
	}
	
	void Update () {
        timer += Time.deltaTime;

        if (timer > skillDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    private void SpawnFireball()
    {
        float randomX, randomY, randomZ;
        randomX = Random.Range(this.transform.position.x - 0.5f * this.transform.localScale.x, this.transform.position.x + 0.5f * this.transform.localScale.x);
        randomY = Random.Range(this.transform.position.y - 0.5f * this.transform.localScale.y, this.transform.position.y + 0.5f * this.transform.localScale.y);
        randomZ = Random.Range(this.transform.position.z - 0.5f * this.transform.localScale.z, this.transform.position.z + 0.5f * this.transform.localScale.z);

        GameObject fireball = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Fire/Fireball"));

        fireball.transform.position = new Vector3(randomX, randomY, randomZ);

        randomX = Random.Range(-0.05f, 0.05f);
        randomY = Random.Range(-0.4f, -0.6f);
        randomZ = Random.Range(-0.05f, 0.05f);

        fireball.GetComponent<FireballBehavior>().direction = new Vector3(randomX, randomY, randomZ);
    }
}
