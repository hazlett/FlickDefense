using UnityEngine;
using System.Collections;

public class Flick : MonoBehaviour {

    public FlickBehaviour behaviour;
    private Vector2 velocity;
    private Vector3 screenToWorld, touchToWorld;
    void Start () {
	    
	}
	
    void OnEnable()
    {

    }

	void Update () {
	}

    public void SetPosition(Vector2 position)
    {
        rigidbody.useGravity = false;
        touchToWorld = position;
        touchToWorld.z = transform.position.z + Camera.main.nearClipPlane;
        screenToWorld = Camera.main.ScreenToWorldPoint(touchToWorld);
        transform.position = screenToWorld;
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
        rigidbody.AddForce(velocity);
        rigidbody.useGravity = true;
        this.enabled = true;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 500.0f, 50.0f), "FLICKED: " + velocity);
        GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f + 75.0f, 500.0f, 50.0f), "Touch to world: (" + touchToWorld.x + ", " + touchToWorld.y + ", " + touchToWorld.z + ")");
        GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f + 150.0f, 500.0f, 50.0f), "Screen to world: (" + screenToWorld.x + ", " + screenToWorld.y + ", " + screenToWorld.z + ")");
    }
}
