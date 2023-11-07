using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    PlayerControllerTesting playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Marker"))
        {
            uiManager.InteractTextVisibility(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Marker"))
        {
            Debug.Log("Entered");
            if (collision.gameObject.GetComponent<MarkerCheckTemp>().isLever)
                uiManager.InteractTextVisibility(true);
        }
    }
}
