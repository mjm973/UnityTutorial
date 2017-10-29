using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All scripts in our GameObjects must inherit from the MonoBehaviour class: this is to ensure they work within the Unity engine.
public class EarthScript : MonoBehaviour {
    // Variables we declare as public will show on the Editor's Inspector
    // This means we can easily modify them from there!
    // NOTE: Editor values will override the values we hard-code, so be careful.

    public bool rotate = true; // Is our Earth rotating?

    // The Range tag turns our variable into a slider in the Editor.
    [Range(0, 360)]
    public float angularSpeed = 90; // How fast will Earth rotate?

    // We can also use public variables to get references to other GameObjects by assigning them in the Editor
    public GameObject moon;

    // It is handy to keep references to Components we'll be accessing often.
    MeshRenderer mesh;

    // Start is called once when the GameObject is first activated or instantiated.
    void Start() {
        // To access Components from within our scripts, we use the GetComponent method:
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame. Note that this means that the time between Update calls depends on our frame rate!
    void Update() {

        // Check our boolean to decide if we should rotate
        if (rotate) {
            // A GameObject's own Transform is by default accessible as transform.
            // We can use transform to translate, rotate, or scale GameObjects

            // transform.Rotate(0, -angularSpeed, 0); // This will spin too fast because we are rotating X degrees per frame!

            // By multiplying our angular speed by Time.deltaTime, we scale the rotation depending on the duration of the frame
            // In other words, we are now rotating X degrees per second.
            transform.Rotate(0, -angularSpeed * Time.deltaTime, 0);
        }

        // We can check for mouse interactions using the scene's main camera and some raycasting
        // The Input class gives us access to input data, including keyboard and mouse

        // GetMouseButton will be true if the button is currently pressed, so it might fire multiple frames
        if (Input.GetMouseButton(0)) {
            Vector2 mousePos = Input.mousePosition;

            // The main camera is always accessible through the Camera class.
            // We can use it to trace a ray to where our mouse is pointing.
            Ray ray = Camera.main.ScreenPointToRay((Vector3)mousePos);

            // Finally, we can check if the ray hit our Earth!
            // The Physics class allows us to raycast: trace a ray and check if it hit a collider. It will return true if we hit something.
            bool hit = Physics.Raycast(ray);

            // Let's test this by changing Earth's color:
            if (hit) {
                mesh.material.color = Color.red;
            }
            else {
                mesh.material.color = Color.white;
            }
        } else {
            mesh.material.color = Color.white;
        }

        // GetMouseButtonDown will be true only on the frame we first clicked
        if (Input.GetMouseButtonDown(0)) {
            // We cast our ray once...
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay((Vector3)mousePos);
            bool hit = Physics.Raycast(ray);

            // And spawn a Moon if the Earth is clicked!
            if (hit) {
                // Instantiate spawns a GameObject, at a position, with a rotation. Quaternion.identity means we don't want to rotate anything.
                Instantiate(moon, transform.position + Vector3.right * 5f, Quaternion.identity);
            }
        }
    }
}
