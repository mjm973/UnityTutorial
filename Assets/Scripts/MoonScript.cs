using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {

    // We'll keep a reference to the Earth.
    GameObject earth;

    MeshRenderer mesh;
    // The Rigidbody component enables physics for the GameObject
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        // We can look up GameObjects in the scene by name (Find) or tag (FindGameObjectByTag)
        earth = GameObject.Find("Earth");

        mesh = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        // Let's give the Moon a random initial speed to start orbiting
        float magnitude = Random.Range(5, 10);
        // To determine the direction, we'll take the cross product of the vector pointing towards Earth, and up
        Vector3 toEarth = earth.transform.position - transform.position;
        Vector3 direction = Vector3.Cross(toEarth, Vector3.up).normalized;

        // Finally, let's assign this velocity to our Rigidbody
        rb.velocity = direction * magnitude;
	}
	
	// To make sure our physics calculations run smoothly, let's use FixedUpdate
    // FixedUpdate is called at a fixed interval, regardless of framerate.
	void FixedUpdate () {
        // We'll add a centripetal force to keep the moon orbiting
        // First, direction
        Vector3 direction = earth.transform.position - transform.position;

        // Then, magnitude. Let's just make it proportional to the inverse distance squared
        float distanceSquared = direction.sqrMagnitude;
        float magnitude = 200f / distanceSquared;

        // Finally, we add the force to our Moon
        rb.AddForce(direction.normalized * magnitude);
	}

    // OnCollisionEnter fires whenever our physics-enabled GameObject starts colliding with something
    private void OnCollisionEnter(Collision collision) {
        // Let's turn moons that have collided blue, for instance
        mesh.material.color = Color.blue;
    }
}
