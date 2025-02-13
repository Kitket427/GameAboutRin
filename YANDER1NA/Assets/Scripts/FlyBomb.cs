using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBomb : MonoBehaviour
{
    private float rotate;
    private Aim aim;
    [SerializeField] private float speed;
    private float speedCount;
    private Transform target;
    void Start()
    {
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        aim = GetComponentInParent<Aim>();
    }
    void Update()
    {
        if (target.position.x < transform.position.x)
        {
            if (speedCount > -1) speedCount -= Time.deltaTime * 2;
            else speedCount = -1;
        }
        else
        {
            if (speedCount < 1) speedCount += Time.deltaTime * 2;
            else speedCount = 1;
        }
        rotate -= Time.deltaTime * speedCount * speed;
        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }
}
