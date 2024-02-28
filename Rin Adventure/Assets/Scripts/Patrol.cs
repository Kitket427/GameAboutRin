using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Vector2[] patrolPos;
    [SerializeField] private float speed, animSpeed;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("move", true);
        anim.speed = speed * 0.03f;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * transform.localScale.x, rb.velocity.y);
    }
    private void Update()
    {
        if(transform.position.x < patrolPos[0].x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (transform.position.x > patrolPos[1].x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
