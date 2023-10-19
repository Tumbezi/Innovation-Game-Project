using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public GameObject currentTarget;
    static public GameObject nextTarget;
    public Vector3 startPosition;
    Rigidbody rb;

    void Start()
    {
        // Get the start position for later Vector3.Lerp?
        startPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check which input player presses     (Up, Down, Left, Right)
        // Inputs have values presenting them   (1   2     3     4    )
        // Go fetch information from MarkerCheck with the function getRoutes(int input)
        // Information contains a possible location and if it's possible to move there...
        // If the information retrieved comes out TRUE then GO to the POSITION that we just GOT!!!
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(1))
            {
                transform.position = nextTarget.transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(2))
            {
                transform.position = nextTarget.transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(3))
            {
                transform.position = nextTarget.transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(4))
            {
                transform.position = nextTarget.transform.position;
            }
        }
    }

    // Self explanatory
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marker"))
        {
            currentTarget = other.gameObject;
        }
    }
}
