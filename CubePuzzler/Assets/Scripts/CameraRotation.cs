using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Camera camera_;
    public Transform target;
    public float zoomin;
    private Vector3 prevPos;

    // Set inital camera position
    private void Start()
    {
        camera_.transform.position = target.position;
        camera_.transform.Translate(new Vector3(0, 0, zoomin));
    }

    private void Update()
    {
        // Grab the location of the camera position
        if (Input.GetMouseButtonDown(0))
        {
            prevPos = camera_.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            // Get direction of the camera from screen space
            Vector3 direction = prevPos - camera_.ScreenToViewportPoint(Input.mousePosition);
            camera_.transform.position = target.position;


            // Camera rotation by the y axis (experiemental :P) only on X-axis rn
            //camera_.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            camera_.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180);
            camera_.transform.Translate(new Vector3(0, 0, zoomin));
            prevPos = camera_.ScreenToViewportPoint(Input.mousePosition);

            // Scroll forward or backwards to zoom
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) zoomin++;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) zoomin--;
        }
    }
}
