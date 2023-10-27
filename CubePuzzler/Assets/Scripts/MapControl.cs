using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    private float rotationSpeed = 10;
    public bool enableMouse = false;


    // TODO. Rotation with the WASD / Arrow Keys. Add +rotation to the direction
    // we want to show

    private void Update()
    {
        if (!enableMouse)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.Rotate(Vector3.right, 90);
                Debug.Log("W");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(Vector3.up, 90);
                Debug.Log("A");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.Rotate(Vector3.left, 90);
                Debug.Log("S");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(Vector3.down, 90);
                Debug.Log("D");
            }
        }
    }

    private void OnMouseDrag()
    {
        if (enableMouse)
        {
            float xAxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.down, xAxisRotation);
            transform.Rotate(Vector3.left, yAxisRotation);
        }
    }
}