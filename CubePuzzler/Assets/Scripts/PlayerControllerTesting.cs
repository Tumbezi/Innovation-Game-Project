using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    public CameraController cc;
    public bool cameraFar;

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

    }

    GameObject CheckMoveDirection(Vector3 direction)
    {
        //Cast raycast to direction where player wants to move
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //If raycast hits Marker, return that gameobject
            if (hit.collider.CompareTag("Marker"))
                return hit.collider.gameObject;
            else
                return null;
        }
        //If raycast doesn't hit anything, check if current Marker is edge
        else if (currentTarget.GetComponent<MarkerCheckTemp>().isEdge)
        {
            CheckIfRotating(currentTarget.GetComponent<MarkerCheckTemp>());
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
        //Time in which player can't move
        moving = true;
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }

    void CheckIfRotating(MarkerCheckTemp mc)
    {
        /*
        if (dir.x == 1f)
            RotateView(Edgeside.Right);
        else if (dir.x == -1f)
            RotateView(Edgeside.Left);
        else if (dir.y == 1f)
            RotateView(Edgeside.Top);
        else if (dir.y == -1f)
            RotateView(Edgeside.Bottom);
        */
        
        //Check which rotation from MarkerCheckTemp is the same as player currently has and call RotateView with the not matching side
        if (transform.rotation.x == mc.sides[0].rotation.x && transform.rotation.y == mc.sides[0].rotation.y)
            RotateView(mc.sides[1]);
        else
            RotateView(mc.sides[0]);
        
        /*
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
        */
    }

    void RotateView(Transform side)
    {
        //Rotate the player to new side
        //If cameraFar is true rotate camera separetly
        transform.rotation = side.transform.rotation;
        if (cameraFar) cc.RotateCamera(side);
        StartCoroutine(MoveTimer());
        
    }


}
