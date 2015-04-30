using UnityEngine;
using System.Collections;

public class Flick : MonoBehaviour {

    public EnemyBehaviour behaviour;
    private Vector3 velocity;
    private Vector3 screenToWorld, touchToWorld;
    private Vector3 screen, world;
    private float fallHeight;
    private bool falling;

	
    void OnEnable()
    {
        falling = false;
        fallHeight = 0;
    }
    
	void Update () {
        screen = Camera.main.WorldToScreenPoint(transform.position);
        screen.x = Mathf.Clamp(screen.x, 0, Screen.width);
        //screen.y = Mathf.Clamp(screen.y, 0.0f, Screen.height);
        world = Camera.main.ScreenToWorldPoint(screen);
        world.z = Mathf.Clamp(transform.position.z, -5.0f, 25.0f);
        transform.position = world;
        if ((GetComponent<Rigidbody>().velocity.y < 0) && (!falling))
        {
            falling = true;
            fallHeight = transform.position.y;
        }
	}

    public void SetPosition(Vector2 position)
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        behaviour.enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        touchToWorld = position;
        touchToWorld.z = transform.position.z - Camera.main.transform.position.z;
        screenToWorld = Camera.main.ScreenToWorldPoint(touchToWorld);
        screenToWorld.z = transform.position.z;
        transform.position = screenToWorld;

    }

    public void SetVelocity(Vector2 velocity)
    {
        behaviour.enabled = false;
        this.velocity = velocity;
        this.velocity.z = velocity.magnitude * 0.25f * transform.forward.z;
        GetComponent<Rigidbody>().velocity = this.velocity;
        GetComponent<Rigidbody>().useGravity = true;
        this.enabled = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            behaviour.enabled = true;
            behaviour.Landed(fallHeight);
            this.enabled = false;
        }
    }
    void OnGUI()
    {
        //GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 500.0f, 50.0f), "FLICKED: " + velocity);
        //GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f + 75.0f, 500.0f, 50.0f), "Touch to world: (" + touchToWorld.x + ", " + touchToWorld.y + ", " + touchToWorld.z + ")");
        //GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f + 150.0f, 500.0f, 50.0f), "Screen to world: (" + screenToWorld.x + ", " + screenToWorld.y + ", " + screenToWorld.z + ")");
    }
}
