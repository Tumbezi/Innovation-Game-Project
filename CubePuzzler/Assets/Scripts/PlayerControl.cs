using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerControl : MonoBehaviour
{ 
    public SkyboxManager skyboxManager;
    public LayerMask marker;
    private PlayerActions playerActions;
    private Vector3 currentMoveDirection;
    public GameObject currentTarget, travelStartTarget;
    public float moveTime, speed;
    static public GameObject nextTarget;
    public bool moving = false;
    [SerializeField]
    float waitTime;
    public CameraController cc;
    public UIController uiController;

    [SerializeField]
    int playerHealth;

    private Transform currentSide;

    private void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Default.MoveRight.performed += ctx => MovePressed(transform.right);
        playerActions.Default.MoveLeft.performed += ctx => MovePressed(transform.right * -1);
        playerActions.Default.MoveUp.performed += ctx => MovePressed(transform.up);
        playerActions.Default.MoveDown.performed += ctx => MovePressed(transform.up * -1);
        playerActions.Default.Interact.performed += ctx => Interact();
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
        if (!moving)
        {
            travelStartTarget = currentTarget;
            CheckMoveDirection(direction);
        }
    }

    // Update is called once per frame
    /* Not used for now, so commented off
    void Update()
    {

    }
    */

    void CheckMoveDirection(Vector3 direction)
    {
        currentMoveDirection = direction;
        //Cast raycast to direction where player wants to move
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //If raycast hits Marker, return that gameobject
            if (hit.collider.CompareTag("Marker"))
            {
                currentTarget = hit.collider.gameObject;
                StartCoroutine(MovePlayer(currentTarget));
            }
            else
                moving = false;

        }
        //If raycast doesn't hit anything, check if current Marker is edge
        else if (currentTarget.GetComponent<MarkerCheck>().isEdge)
        {
            CheckIfRotating(currentTarget.GetComponent<MarkerCheck>());
            moving = false;
        }
        else
            moving = false;
    }

    IEnumerator MovePlayer(GameObject target)
    {
        if (target != null)
        {
            
            moving = true;
            float timeElapsed = 0f;
            Vector3 start = transform.position;

            while (timeElapsed <= moveTime)
            {
                //Move player by lerping the new position
                Vector3 newPos = Vector3.Lerp(start, target.transform.position, timeElapsed);
                transform.position = newPos;
                timeElapsed += Time.deltaTime * speed;
                yield return null;
            }

            //Ensure right movement by setting player to the right position
            transform.position = target.transform.position;
            currentTarget = target;

            //If new current marker is ice marker, try to move player again to same direction
            if (target.GetComponent<MarkerCheck>().isIce)
                CheckMoveDirection(currentMoveDirection);
            else if (target.GetComponent<MarkerCheck>().isHazard)
            {
                transform.position = travelStartTarget.transform.position;
                currentTarget = travelStartTarget;
                moving = false;
                ChangeHealth(-1);
            }
            else
                moving = false;
        }
    }

    IEnumerator MoveTimer()
    {
        //Time in which player can't move
        moving = true;
        yield return new WaitForSeconds(waitTime);
        moving = false;
    }

    void CheckIfRotating(MarkerCheck mc)
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

        //Check which rotation from MarkerCheck is the same as player currently has and call RotateView with the not matching side
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

    void Interact()
    {
        if (currentTarget.GetComponent<MarkerCheck>().isLever && !moving)
        {
            currentTarget.GetComponent<MarkerCheck>().OpenGate();
        }
    }

    void RotateView(Transform side)
    {
        //TODO! Add player rotation and restrictions when currenlty rotating

        //Rotate the player to new side
        transform.rotation = side.transform.rotation;

        // Reset camera position if rotation is made.
        currentSide = side;
        cc.RotateCamera(side);
        /*
        cc.transform.position = currentSide.position;
        cc.transform.rotation = currentSide.rotation;
        cc.transform.Translate(0, 0, CameraRotation.zoomin);
        */
        skyboxManager.ChangeSkybox();
    }

    // This gets currentside from RotateView to use in other scripts
    public Transform FetchSide()
    {
        return currentSide;
    }

    void ChangeHealth(int change)
    {
        playerHealth += change;
        uiController.SetHealthIcons(playerHealth);
    }
}