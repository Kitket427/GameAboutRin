using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAce : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float posL, posR, speed, curSpeed, rotate;
    [SerializeField] private bool right;
    [SerializeField] private Transform target;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * curSpeed, 0);
        transform.rotation = Quaternion.Euler(0, 0, curSpeed * -rotate);
    }
    private void Update()
    {
        if (target) posR = posL = target.position.x;
        if (posR < transform.position.x)
        {
            right = false;
        }
        if(posL > transform.position.x)
        {
            right = true;
        }
        if(right)
        {
            if (curSpeed < 1) curSpeed += Time.deltaTime * 2;
            else curSpeed = 1;
        }
        else
        {
            if (curSpeed > -1) curSpeed -= Time.deltaTime * 2;
            else curSpeed = -1;
        }
    }
}
