using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [SerializeField]
    UIController uiController;

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
            uiController.InteractTextVisibility(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Marker"))
        {
            if (collision.gameObject.GetComponent<MarkerCheck>().isLever)
                uiController.InteractTextVisibility(true);
        }
    }
}
