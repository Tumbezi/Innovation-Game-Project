using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraRotator;
    public float rotSpeed;

    public void RotateCamera(Transform side)
    {
        StartCoroutine(RotationAnimation(cameraRotator.transform, side));
        //cameraRotator.transform.rotation = side.rotation;
    }

    IEnumerator RotationAnimation(Transform current, Transform target)
    {
        Quaternion startRot = current.rotation;
        Quaternion targetRot = target.rotation;
        float timeElapsed = 0f;
        Vector3 start = transform.localScale;

        while (timeElapsed <= 1f)
        {
            //Lerp new value for axis and scale gate by it
            Quaternion newRot = Quaternion.Lerp(startRot, targetRot, timeElapsed);
            cameraRotator.transform.rotation = newRot;
            timeElapsed += Time.deltaTime * rotSpeed;
            yield return null;
        }

        //Ensure that the scale is correct
        cameraRotator.transform.rotation = targetRot;

    }
}
