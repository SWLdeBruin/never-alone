using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    private float multiplier = 2.82f;

    private void Awake()
    {
        Camera.main.orthographicSize = Screen.height / Screen.width * multiplier;
    }
}
