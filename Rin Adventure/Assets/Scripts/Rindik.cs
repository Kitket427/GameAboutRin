using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rindik : MonoBehaviour
{
    private Animator[] anim;
    private Rigidbody2D rb;
    [SerializeField] private float speed, movespeed;
    private void Start()
    {
        anim = GetComponentsInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movespeed * speed, rb.velocity.y);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false)
        {
            movespeed = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow))
        {
            movespeed = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else movespeed = 0;
        if (rb.velocity.x > -0.1f && rb.velocity.x < 0.1f) anim[0].SetBool("move", false);
        else anim[0].SetBool("move", true);
        if (Input.GetKey(KeyCode.X))
        {
            anim[1].SetBool("attack", true);
        }
        else
        {
            anim[1].SetBool("attack", false);
        }
    }
}
