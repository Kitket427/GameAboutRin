using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float[] patrolPos;
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
        if(transform.position.x < patrolPos[0])
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (transform.position.x > patrolPos[1])
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
