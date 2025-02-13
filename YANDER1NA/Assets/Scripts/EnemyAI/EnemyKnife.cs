using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float[] patrolPos;
    [SerializeField] private float speed, animSpeed, boost, startSpeed, noAttack = 1, timeAttack, time;
    [SerializeField] private GameObject warning;
    [SerializeField] private bool attack;
    [SerializeField] private Transform player;
    [SerializeField] private bool playerMoment;
    [SerializeField] private TrailEffect trail;
    [SerializeField] private GameObject sfx;
    private float extra;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("move", true);
        anim.speed = speed * 0.03f;
        startSpeed = speed;
        if (boost > 0) Reload();
        timeAttack = 2;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * transform.localScale.x * noAttack, rb.velocity.y);
    }
    private void Update()
    {
        if(transform.position.x < patrolPos[0])
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (boost > 0) Reload();
        }
        if (transform.position.x > patrolPos[1])
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (boost > 0) Reload();
        }
        if(attack)
        {
            time += Time.deltaTime;
            if (time > timeAttack + 0.3f)
            {
                warning.SetActive(false);
                anim.SetTrigger("attack");
                anim.SetBool("move", false);
                noAttack = 0;
                Invoke(nameof(Attack), 1.5f);
                timeAttack = Random.Range(2.2f, 4.4f);
                time = 0;
            }
            else if (time > timeAttack)
            {
                warning.SetActive(true);
                playerMoment = false;
            }
        }
        if (player)
        {
            if (playerMoment && Vector2.Distance(transform.position, player.position) < 35)
            {
                if (time > 2) time = 2;
                timeAttack = 2.1f;
                playerMoment = false;
                extra = 7;
            }
        }
    }
    void Reload()
    {
        speed = startSpeed + Random.Range(0, boost);
        anim.speed = speed * 0.03f;
    }
    void Attack()
    {
        anim.SetBool("move", true);
        if(Random.Range(1, (12+extra)) < 6) noAttack = 1;
        else
        {
            noAttack = 5;
            trail.enabled = true;
            sfx.SetActive(true);
            Invoke(nameof(StopDash), Random.Range(0.2f, 0.7f));
            extra = 0;
        }
        playerMoment = true;
    }
    void StopDash()
    {
        noAttack = 1;
        trail.enabled = false;
        sfx.SetActive(false);
    }
}
