using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public GameObject cameraRotator;

    public void RotateCamera(Transform side)
    {
        cameraRotator.transform.rotation = side.rotation;
    }
}
