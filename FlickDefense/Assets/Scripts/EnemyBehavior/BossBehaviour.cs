using UnityEngine;
using System.Collections;

public class BossBehaviour : EnemyBehaviour {
    public ScreenRock rock;
    public GameObject throwRock;
    private bool throwing, rotating, rotatingBack, hasRock;
    private Vector3 castleDoorLocation;
    void Start()
    {
        health = 5;
        castleDoorLocation = GameObject.Find("CastleDoor").transform.position;
        moveLocation = castleDoorLocation + new Vector3(1.0f, 0, 0);
        GameObject location = GameObject.Instantiate(Resources.Load("Prefabs/Locations/PersonalLocation")) as GameObject;
        location.transform.position = moveLocation;
        location.GetComponent<PersonalLocationManager>().SetObject(gameObject);
        speed *= 0.5f;
        agent.speed = speed;
        throwing = false;
        rotating = false;
        rotatingBack = false;
        hasRock = true;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (rotating)
        {
            ZeroForces();
            if (rotatingBack)
            {
                Quaternion rotation = Quaternion.LookRotation(castleDoorLocation - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 50.0f * Time.deltaTime);
                if (Approx(transform.rotation.eulerAngles, rotation.eulerAngles, 5.0f))
                {
                    rotatingBack = false;
                    agent.enabled = true;
                    rotating = false;
                    if (atLocation)
                    {
                        InvokeRepeating("Attack", 1, 3);
                    }
                }
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((Camera.main.transform.position - transform.position)), 50.0f * Time.deltaTime);
                if (Approx(transform.localRotation.eulerAngles, Quaternion.LookRotation((Camera.main.transform.position - transform.position)).eulerAngles) && !throwing)
                {
                    animator.SetFloat("Speed", 0.0f);
                    StartCoroutine("ThrowAtScreen");
                }
                else
                {
                    animator.SetFloat("Speed", speed);
                }
            }
        }
        else if (!throwing)
        {
            if (!atLocation)
            {
                agent.SetDestination(moveLocation);
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.Stop();
                ZeroForces();
                animator.SetFloat("Speed", 0);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                LookAtScreen();
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (hasRock)
                {
                    animator.SetFloat("Speed", 0.0f);
                    StartCoroutine("ThrowAtWall");
                }
            }
        }
    }
    void OnGUI()
    {
        GUILayout.Label("Boss Health: " + health);
    }
    public void AbortThrow()
    {
        rotatingBack = true;
        throwing = false;
        hasRock = false;
    }
    private void LookAtScreen()
    {
        if (hasRock)
        {
            rock.EnableExplode(this);
            CancelInvoke("Attack");
            agent.enabled = false;
            rotating = true;
        }
    }
    private bool Approx(Vector3 one, Vector3 two, float compare = 0.1f)
    {
        if ((one.x <= two.x + compare) && (one.x >= two.x - compare) && (one.y <= two.y + compare) && (one.y >= two.y - compare) && (one.z <= two.z + compare) && (one.z >= two.z - compare))
        { return true; }
        else { return false; }
    }
    private IEnumerator ThrowAtWall()
    {
        rock.SetEndLocation(castleDoorLocation);
        hasRock = false;
        throwing = true;
        agent.enabled = false;
        if (throwRock != null)
        {
            throwRock.SetActive(true);
        }
        rock.EnableExplode(this);
        animator.SetTrigger("ThrowRock");
        yield return new WaitForSeconds(1.2f);
        agent.enabled = true;
        throwing = false;
    }
    private IEnumerator ThrowAtScreen()
    {
        hasRock = false;
        throwing = true;
        agent.enabled = false;
        if (throwRock != null)
        {
            throwRock.SetActive(true);
        }
        animator.SetTrigger("ThrowRock");
        yield return new WaitForSeconds(1.2f);
        rotatingBack = true;
        throwing = false;
    }
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine("DamageWall");       
        timer = 0;
    }
    private IEnumerator DamageWall()
    {
        yield return new WaitForSeconds(0.5f);
        UserStatus.Instance.DamageCastle();
    }
    public override void AtLocation()
    {
        ZeroForces();
        animator.SetTrigger("AtLocation");
        atLocation = true;
        InvokeRepeating("Attack", 1, 3);
    }
    public override void OffLocation()
    {
        atLocation = false;
        CancelInvoke("Attack");
    }
    private void ZeroForces()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    public void Tap()
    {
       if (hasRock)
       {
           LookAtScreen();
       }
    }
}
