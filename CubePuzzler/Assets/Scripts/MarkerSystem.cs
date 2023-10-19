using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject currentSpot;
    public GameObject[] spots;
    public bool cango;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        cango = currentSpot.GetComponent<MarkerCheck>().getRoutes(1);
        Debug.Log(cango);
        */
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
    */
}
