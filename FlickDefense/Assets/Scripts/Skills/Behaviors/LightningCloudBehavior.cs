using UnityEngine;
using System.Collections;

public class LightningCloudBehavior : MonoBehaviour {

    internal int damage = 5;
    internal bool level1 = true;

    private int strikeCount = 0;

	void Start () {

        if (level1)
        {
            Invoke("Attack", 2.0f);
        }
        else
        {
            InvokeRepeating("Attack", 2.0f, 2.0f);
        }
	}

    private void Attack()
    {
        if (strikeCount < 3)
        {
            Debug.Log("ATTACK");
            GameObject lightning = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Skills/Lightning/LightningStrike"));

            lightning.transform.position = this.transform.position + new Vector3(0, -1.0f, 0);
            lightning.transform.parent = this.transform;

            this.transform.parent.GetComponent<EnemyBehaviour>().Damage(damage);

            strikeCount++;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
