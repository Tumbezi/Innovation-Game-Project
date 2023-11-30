using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Camera camera_;
    private Transform target;
    private bool targetSwap;
    private bool rotateMode;
    public Transform targetMap;
    public Transform targetPlayer;
    public static float zoomin;
    private Vector3 prevPos;

    public Transform startSide;
    public Transform currentSide;
    public GameObject player;
    private PlayerControl pc;

    // Set inital camera position
    private void Start()
    {
        zoomin = -32.35f;
        target = targetMap;
        camera_.transform.position = target.position;
        camera_.transform.Translate(new Vector3(0, 0, zoomin));
        pc = player.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        // Swap camera between player and map
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (targetSwap)
                target = targetMap;
            else
                target = targetPlayer;

            targetSwap =! targetSwap;
        }

        // Grab the location of the camera position
        // Toggle camera rotation with C
        if (Input.GetKeyDown(KeyCode.C))
            rotateMode =! rotateMode;

        if (rotateMode)
        {
            RotateCamera();
            prevPos = camera_.ScreenToViewportPoint(Input.mousePosition);
            Cursor.visible = false;
        }

        else if (!rotateMode)
        {
            prevPos = camera_.ScreenToViewportPoint(Input.mousePosition);
            Cursor.visible = true;
        }
            

    }

    // Rotate the camera
    void RotateCamera()
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

    // Gets the currently used side and transforms the camera to that position
    public void ResetCamera()
    {
        // We need to set the default side in order to remove 15+ lines of code...  Currently the "FetchSide()" which gathers information from "RotateView()"
        // doesn't have a default value for the initial return. It retuns "null". Also doesn't return the top camera position, so it cannot be reset to that.
        // In the meanwhile, we just catch the error including the null Transform and give it a default value while that's going on.
        if (currentSide == null)
        {
            camera_.transform.position = startSide.position;
            camera_.transform.rotation = startSide.rotation;
            camera_.transform.Translate(new Vector3(0, 0, zoomin));

            try
            {
                currentSide = pc.FetchSide();
            }
            catch (System.Exception e)
            {
                // We've received a null Transform! Throw it away, we do not care.
                throw;
            }
        }
        else
        {
            currentSide = pc.FetchSide();
            camera_.transform.position = currentSide.position;
            camera_.transform.rotation = currentSide.rotation;
            camera_.transform.Translate(new Vector3(0, 0, zoomin));
        }
    }
}




/*
if (!targetSwap)
{
    if (Input.GetMouseButtonDown(0))
        prevPos = camera_.ScreenToViewportPoint(Input.mousePosition);

    if (Input.GetMouseButton(0))
        RotateCamera();

    if (Input.GetMouseButton(1))
        ResetCamera();
}

else
{
    RotateCamera();
}
*/
