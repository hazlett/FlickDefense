using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    protected Vector3 moveLocation;
    protected float speed = 3.5f, timer;
    protected bool atLocation;
    public NavMeshAgent agent;
	void Start () {
        moveLocation = GameObject.Find("CastleDoor").transform.position;
        atLocation = false;
        agent.SetDestination(moveLocation);
	}

    void OnEnable()
    {
        agent.enabled = true;
    }
    void OnDisable()
    {
        agent.enabled = false;
    }
	void Update () {
        if (!atLocation)
        {
            agent.SetDestination(moveLocation);
        }
    }

    protected virtual void Attack()
    {
        Debug.Log("Generic Attack");
        UserStatus.Instance.DamageCastle();
        timer = 0;
    }
    public virtual void AtLocation()
    {
        atLocation = true;
        InvokeRepeating("Attack", 1, 3);
    }
    public void OffLocation()
    {
        atLocation = false;
    }

    void OnGUI()
    {
        GUILayout.Box("HEALTH: " + UserStatus.Instance.CastleHealth);
    }
}
