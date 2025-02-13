using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustFlip : MonoBehaviour
{
    private Transform pl;
    private void Start()
    {
        OnEnable();
    }
    void Update()
    {
        if (pl.position.x < transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
    }
    private void OnEnable()
    {
        pl = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
    }
}
