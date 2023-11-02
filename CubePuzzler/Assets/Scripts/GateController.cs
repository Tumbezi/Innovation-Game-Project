using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public enum AxisToScale
{
    X,
    Y,
    Z
}

public class GateController : MonoBehaviour
{
    public AxisToScale axis;
    public float startScale, targetScale, scaleTime;
    public bool isOpen, isScaling;
    Vector3 startScaleVector;
    // Start is called before the first frame update
    void Start()
    {
        startScaleVector = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GateAction()
    {
        if (!isScaling)
        {
            if (!isOpen)
            {
                //Determine is which axis the gate is moving and start Coroutine with correct axis
                switch (axis)
                {
                    case AxisToScale.X:
                        StartCoroutine(ActionAnimation(new Vector3(targetScale, startScale, startScale))); break;
                    case AxisToScale.Y:
                        StartCoroutine(ActionAnimation(new Vector3(startScale, targetScale, startScale))); break;
                    case AxisToScale.Z:
                        StartCoroutine(ActionAnimation(new Vector3(startScale, startScale, targetScale))); break;
                }
            }
            else
                StartCoroutine(ActionAnimation(new Vector3(startScale, startScale, startScale)));

            isOpen = !isOpen;
        }
    }

    IEnumerator ActionAnimation(Vector3 target)
    {
        isScaling = true;
        float timeElapsed = 0f;
        Vector3 start = transform.localScale;

        while (timeElapsed <= scaleTime)
        {
            //Lerp new value for axis and scale gate by it
            Vector3 newScale = Vector3.Lerp(start, target, timeElapsed);
            transform.localScale = newScale;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //Ensure that the scale is correct
        transform.localScale = target;
        isScaling= false;
    }
}



