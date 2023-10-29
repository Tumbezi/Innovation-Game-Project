using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTesting : MonoBehaviour
{
    private PlayerActions playerActions;
    private Vector2 moveInput;
    public GameObject currentTarget;
    public float speed;
    static public GameObject nextTarget;
    public bool moving = false;
    [SerializeField]
    float waitTime;

    private void Awake()
    {
        playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        playerActions.Default.Enable();
    }

    private void OnDisable()
    {
        playerActions.Default.Disable();
    }

    void Start()
    {
        transform.position = currentTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving) { 
            moveInput = playerActions.Default.Movement.ReadValue<Vector2>();
            if (moveInput != Vector2.zero)
            {
                MovePlayer(CheckMoveDirection(moveInput));
            }
        }
        moveInput = Vector2.zero;

        Debug.Log(moveInput.ToString());
        /*
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
        */
    }

    GameObject CheckMoveDirection(Vector2 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Marker"))
                return hit.collider.gameObject;
            else
                return null;
        }
        else
            return null;
    }

    void MovePlayer(GameObject target)
    {
        if (target != null)
        {
            transform.position = target.transform.position;
            moving = true;
            StartCoroutine(MoveTimer());
        }
    }

    IEnumerator MoveTimer()
    {
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }
}
