using UnityEngine;
using System.Collections;

public class SnowCloudBehavior : MonoBehaviour {

    public float slowAmount = 5;

    internal bool level1;

    private float timer;

	void Start () {

        if (level1)
        {
            this.transform.parent.GetComponent<EnemyBehaviour>().Freeze(slowAmount);
            Invoke("DestroyCloud", 7.5f);
        }
        else
        {
            this.transform.parent.GetComponent<EnemyBehaviour>().Freeze(100);
            Invoke("DestroyCloud", 10);
        }
	}

    void DestroyCloud()
    {
        if (level1)
        {
            this.transform.parent.GetComponent<EnemyBehaviour>().UnFreeze(slowAmount);
        }
        else
        {
            this.transform.parent.GetComponent<EnemyBehaviour>().UnFreeze(100);
        }
        Destroy(this.gameObject);
    }
}
