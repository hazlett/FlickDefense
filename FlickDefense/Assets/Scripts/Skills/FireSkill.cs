using UnityEngine;
using System.Collections;

public class FireSkill : MonoBehaviour {

    private FireSkill instance;
    public FireSkill Instance { get { return instance; } set { instance = value; } }

    public GameObject rainOfFire;

    private int level;
    private float cooldownPeriod, skillDuration, timer;
    private bool usingSkill;

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        if (usingSkill)
        {
            timer += Time.deltaTime;
        }
    }

    internal void SetLocation(Vector3 origin, Vector3 direction){
    }

    internal void UseSkill()
    {
        switch (UserStatus.Instance.FireLevel)
        {
            case 1: Fireball();
                break;
            case 2: FireBlast();
                break;
            case 3: FireStorm();
                break;
            case 4: FireWall();
                break;
            case 5: RainOfFire();
                break;
            default:
                break;
        }
        usingSkill = true;
    }

    private void Fireball() 
    {
        GameObject fireball;
    }

    private void FireBlast() { }

    private void FireStorm() { }

    private void FireWall() { }

    private void RainOfFire()
    {

    }
}
