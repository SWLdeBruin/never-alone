using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour
{
    [SerializeField] private float _creditsSpeed;

    private GameObject _credits;

    private void Start()
    {
        _credits = gameObject;
    }

    private void Update()
    {
        _credits.transform.position += _creditsSpeed * Time.deltaTime * Vector3.up;
    }
}
