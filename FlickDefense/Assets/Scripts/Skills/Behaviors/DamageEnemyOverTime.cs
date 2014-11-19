using UnityEngine;
using System.Collections;

public class DamageEnemyOverTime : MonoBehaviour {

    public int damage;

	void Start () {

        InvokeRepeating("DamageOverTime", 0.0f, 0.99f);
	}

    private void DamageOverTime()
    {
        this.transform.parent.GetComponent<EnemyBehaviour>().Damage(damage);
    }
}
