using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
    protected Vector3 moveLocation;
    protected float speed = 0.05f, timer;
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
        if (atLocation)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                Attack();
            }
        }
        else
        {
            agent.SetDestination(moveLocation);
        }
    }

    protected virtual void Attack()
    {
        GameStateManager.Instance.DamageCastle();
        timer = 0;
    }
    public void AtLocation()
    {
        atLocation = true;
        Attack();
    }
    public void OffLocation()
    {
        atLocation = false;
    }

    void OnGUI()
    {
        GUILayout.Box("HEALTH: " + GameStateManager.Instance.CastleHealth);
    }
}
