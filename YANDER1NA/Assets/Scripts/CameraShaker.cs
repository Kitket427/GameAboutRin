using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        var shakes = FindObjectsOfType<CameraShake>();
        foreach (var item in shakes)
        {
            item.ShakeCamera();
        }
    }
}
