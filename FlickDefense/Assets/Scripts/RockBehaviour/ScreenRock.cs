using UnityEngine;
using System.Collections;

public class ScreenRock : MonoBehaviour {

    private Vector3 endLocation;
    private float speed;
    private float clipping;
    private float offset;
    private GameplayGUI gui;
    public bool explode, thrown;
    public GameObject input;
    public GameObject throwRock;
    private bool atScreen, onScreen;
	void Start () {
        clipping = Camera.main.nearClipPlane;
        speed = 25.0f;
        thrown = false;
        explode = false;
        offset = 0.55f;
        gui = GameObject.Find("GUI").GetComponent<GameplayGUI>();
        endLocation = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.55f, Screen.height * 0.6f, + clipping));
        Physics.IgnoreLayerCollision(8, 10, true);
        atScreen = true;
        onScreen = false;
	}
	void OnDestroy()
    {
        if ((explode) && (onScreen))
        {
            GameObject explosion = Instantiate(Resources.Load("Prefabs/Particles/RockHit"), transform.position, Quaternion.identity) as GameObject;
        }
        else if (explode)
        {
            GameObject explosion = Instantiate(Resources.Load("Prefabs/Particles/BomberExplosion"), transform.position, Quaternion.identity) as GameObject;
        }
        Destroy(throwRock);
    }
	void Update () {
        if (thrown)
        {
            transform.position = Vector3.MoveTowards(transform.position, endLocation, Time.deltaTime * speed);

            if ((Camera.main.WorldToViewportPoint(gameObject.transform.position).z < clipping + offset) && atScreen)
            {
                onScreen = true;
                gui.CrackScreen();
                Destroy(gameObject);
            }
            else if((!atScreen) && Approx(transform.position, endLocation))
            {
                Destroy(gameObject);
            }
        }
	}
    public void SetEndLocation(Vector3 endLocation)
    {
        atScreen = false;
        this.endLocation = endLocation;
    }
    public void EnableExplode()
    {
        gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.drag = 0.0f;
        rigidbody.angularDrag = 0.0f;
        explode = true;
    }
    public void Tapped()
    {
        Destroy(gameObject);
    }
    void OnTriggerExit(Collider collider)
    {
        transform.parent = null;
        thrown = true;
    }
    private bool Approx(Vector3 one, Vector3 two, float compare = 0.1f)
    {
        if ((one.x <= two.x + compare) && (one.x >= two.x - compare) && (one.y <= two.y + compare) && (one.y >= two.y - compare) && (one.z <= two.z + compare) && (one.z >= two.z - compare))
        { return true; }
        else { return false; }
    }
}
