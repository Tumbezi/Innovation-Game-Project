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
        playerActions.Default.MoveRight.performed += ctx => MovePressed(transform.right);
        playerActions.Default.MoveLeft.performed += ctx => MovePressed(transform.right * -1);
        playerActions.Default.MoveUp.performed += ctx => MovePressed(transform.up);
        playerActions.Default.MoveDown.performed += ctx => MovePressed(transform.up * - 1);
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

    void MovePressed(Vector3 direction)
    {
        MovePlayer(CheckMoveDirection(direction));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!moving) { 
            //moveInput = transform.InverseTransformDirection(playerActions.Default.Movement.ReadValue<Vector2>());
            if (moveInput != Vector2.zero)
            {
                Debug.Log(moveInput.ToString());
                MovePlayer(CheckMoveDirection(moveInput));
            }
        }
        moveInput = Vector2.zero;

        
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

    GameObject CheckMoveDirection(Vector3 direction)
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
        else if (currentTarget.GetComponent<MarkerCheckTemp>().edge != Edgeside.None)
        {
            CheckIfRotating(currentTarget.GetComponent<MarkerCheckTemp>().edge, direction);
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
            currentTarget = target;
            StartCoroutine(MoveTimer());
        }
    }

    IEnumerator MoveTimer()
    {
        moving = true;
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }

    void CheckIfRotating(Edgeside side, Vector3 direction)
    {
        Vector3 sideToVector = new Vector3();
        switch (side)
        {
            case Edgeside.Left:
                sideToVector = Vector3.left; break;
            case Edgeside.Right:
                sideToVector = Vector3.right; break;
            case Edgeside.Top:
                sideToVector = Vector3.up; break;
            case Edgeside.Bottom:
                sideToVector = Vector3.down; break;
        }

        if (sideToVector == direction)
        {
            Debug.Log("Rotating true");
            RotateView(side);
        }
    }

    void RotateView(Edgeside side)
    {
        switch (side)
        {
            case Edgeside.Left:
                transform.Rotate(Vector3.up, 90); break;
            case Edgeside.Right:
                transform.Rotate(Vector3.down, 90); break;
            case Edgeside.Top:
                transform.Rotate(Vector3.right, 90); break;
            case Edgeside.Bottom:
                transform.Rotate(Vector3.left, 90); break;
        }
        StartCoroutine(MoveTimer());
    }


}
