using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    // We need to add a custom script to give the user control over a first-person avatar

    // Let's have a float to determine the movement speed
    public float linearSpeed = 5;

    // And another to control the turning speed
    public float angularSpeed = 120;

    // A boolean to invert the vertical rotation
    public bool invertY = true;

	// Use this for initialization
	void Start () {
        // We can change the cursor's lock state to keep it at the center of the screen.
        // This will allow us to use the mouse to look around and point at the center
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        // To move around: transform.Translate
        // First, we'll make an empty vector to store our input
        Vector3 move = new Vector3();

        // We can use input axes (caps sensitive!) to get common input like WASD or arrow keys
        // It will return a (smoothed) value between -1 and 1
        move.x  = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");

        // Normalize and multiply our vector by our linearSpeed to control our speed from the Editor
        move = move.normalized * linearSpeed;

        // Now we can use our combined input to move around!
        // We specify Space.Self to move relative to us (the camera), not the world axes!
        // Don't forget Time.deltaTime to make sure our movement is defined per second (and not per frame)
        transform.Translate(move * Time.deltaTime, Space.Self);


        // To look around: transform.Rotate
        // Again, store our input in an empty vector
        Vector3 turn = new Vector3();

        // Our horizontal mouse movement will turn around the Y Axis
        turn.y = Input.GetAxis("Mouse X");
        // Our vertical mouse movement will turn around the X Axis
        turn.x = Input.GetAxis("Mouse Y");
        // Invert our vertical rotation?
        if (invertY) {
            turn.x *= -1;
        }

        turn *= angularSpeed;

        // And let's turn!
        //transform.Rotate(turn);

        // But that results in a weird camera. 
        // Turns out we need to separate the rotations if we want an intuitive control

        // Horizontal rotation must be relative to the world axes
        Vector3 worldTurn = new Vector3(0, turn.y, 0);
        // Vertical rotation must be relative to the camera's axes
        Vector3 localTurn = new Vector3(turn.x, 0, 0);

        // And now we turn twice:
        transform.Rotate(worldTurn, Space.World);
        transform.Rotate(localTurn, Space.Self);
	}
}