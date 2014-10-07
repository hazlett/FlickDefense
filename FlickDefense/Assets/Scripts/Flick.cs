using UnityEngine;
using System.Collections;

public class Flick : MonoBehaviour {

    public FlickBehaviour flickBehaviour;
    public MonoBehaviour behaviour;
    private Vector3 velocity;
    private Vector3 screenToWorld, touchToWorld;
    private Vector3 screen, world;
    void Start () {
 
	}
	
    void OnEnable()
    {

    }

	void Update () {
        screen = Camera.main.WorldToScreenPoint(transform.position);
        screen.x = Mathf.Clamp(screen.x, 0, Screen.width);
        //screen.y = Mathf.Clamp(screen.y, 0.0f, Screen.height);
        world = Camera.main.ScreenToWorldPoint(screen);
        world.z = Mathf.Clamp(transform.position.z, -5.0f, 25.0f);
        transform.position = world;
	}

    public void SetPosition(Vector2 position)
    {
        behaviour.enabled = false;
        rigidbody.useGravity = false;
        touchToWorld = position;
        touchToWorld.z = transform.position.z + Camera.main.nearClipPlane;
        screenToWorld = Camera.main.ScreenToWorldPoint(touchToWorld);
        screenToWorld.z = transform.position.z;
        transform.position = screenToWorld;
    }

    public void SetVelocity(Vector2 velocity)
    {
        behaviour.enabled = false;
        this.velocity = velocity;
        this.velocity.z = velocity.magnitude * 0.25f * transform.forward.z;
        rigidbody.AddForce(this.velocity);
        rigidbody.useGravity = true;
        this.enabled = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        behaviour.enabled = true;
    }
    void OnGUI()
    {
        //GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 500.0f, 50.0f), "FLICKED: " + velocity);
        //GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f + 75.0f, 500.0f, 50.0f), "Touch to world: (" + touchToWorld.x + ", " + touchToWorld.y + ", " + touchToWorld.z + ")");
        //GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f + 150.0f, 500.0f, 50.0f), "Screen to world: (" + screenToWorld.x + ", " + screenToWorld.y + ", " + screenToWorld.z + ")");
    }
}
