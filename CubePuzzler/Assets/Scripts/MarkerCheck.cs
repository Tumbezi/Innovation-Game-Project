using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCheck : MonoBehaviour
{
    // Mark everything one by one (BAD!!!(???))
    // The boolean which determines if you can move to that direction
    // And the location of the possible game object

    public bool goUp;
    public bool goDown;
    public bool goLeft;
    public bool goRight;

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    // Get information about the possible directions you can go to
    // IF you can go, then return TRUE!!!
    // IF not return FALSE!!!
    // Really shitty way of making this, because nextTarget changes regardless if it's
    // possible to go there yet or not... Fix!!
    public bool getRoutes(int input)
    {
        switch (input)
        {
            case 1:
                Debug.Log("Can go UP? : " + goUp);
                PlayerControl.nextTarget = up;
                return goUp;
            case 2:
                Debug.Log("Can go DOWN? : " + goDown);
                PlayerControl.nextTarget = down;
                return goDown;
            case 3:
                Debug.Log("Can go LEFT? : " + goLeft);
                PlayerControl.nextTarget = left;
                return goLeft;
            case 4:
                Debug.Log("Can go RIGHT? : " + goRight);
                PlayerControl.nextTarget = right;
                return goRight;
            default:
                return false;
        }
    }
}
