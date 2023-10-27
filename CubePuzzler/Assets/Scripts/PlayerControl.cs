using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject player;
    public GameObject currentTarget;
    public float speed;
    static public GameObject nextTarget;
    
    private Rigidbody rb;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float duration = 3f;
    private float elapsedTime;

    public bool moving = false;

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
                //moving = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(2))
            {
                transform.position = nextTarget.transform.position;
                //moving = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {   
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(3))
            {
                transform.position = nextTarget.transform.position;
                //moving = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentTarget.GetComponent<MarkerCheck>().getRoutes(4))
            {
                transform.position = nextTarget.transform.position;
                //moving = true;
            }
        }

        /*
        if (moving)
        {
            StartCoroutine(WaitForMovement());
        }
        */
    }

    // Self explanatory
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marker") && !moving)
        {
            currentTarget = other.gameObject;
            startPosition = other.gameObject.transform.position;
        } 
    }

    /*
    private IEnumerator WaitForMovement()
    {
        while (true)
        {
            elapsedTime += Time.fixedDeltaTime;
            float perComp = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, nextTarget.transform.position, perComp);
            yield return new WaitForSeconds(duration);
            moving = false;
        }
    }
    */
}
