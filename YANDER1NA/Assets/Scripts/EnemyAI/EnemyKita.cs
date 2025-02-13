using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKita : MonoBehaviour
{
    [SerializeField] private int phase, countAttack, countRandom, curCount;
    [SerializeField] private float left, right, leftT, rightT, speed, curSpeed, time, timeX, reloadR, reloadP, reloadMoment, rotate, reloadJump, reloadJumpRandom, jumpForce, randomJump, yPos;
    [SerializeField] private GameObject rocketFire, effectFire, rocketPos, penguin, warning;
    [SerializeField] private bool trailActive, phaseActive, firstPhase = true;
    private Animator anim;
    private Rigidbody2D rb;
    private TrailEffect[] trails;
    [SerializeField] private GameObject trail;
    private Transform target;
    private OstSystem ost;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trails = GetComponentsInChildren<TrailEffect>();
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
        if (ost) ost.Battle();
        anim.SetInteger("phase", phase);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(curSpeed * speed, rb.velocity.y);
        if(curSpeed > 0) anim.speed = curSpeed / 4f + speed / 60f;
        if(curSpeed < 0) anim.speed = -curSpeed / 4f + speed / 60f;
        if(curSpeed == 0) anim.speed = speed / 60f;
    }
    private void Update()
    {
        if (trailActive)
        {
            foreach (var item in trails)
            {
                item.enabled = true;
            }
            trail.SetActive(true);
        }
        else
        {
            foreach (var item in trails)
            {
                item.enabled = false;
            }
            trail.SetActive(false);
        }
        if (phaseActive) time += Time.deltaTime * (1+timeX);
        else
        {
            if (ost) ost.Battle();
            time = 0;
            rightT = right - target.position.x;
            leftT = target.position.x - left;
            if (right - target.position.x > target.position.x - left && trailActive == false)
            {
                trailActive = true;
                transform.localScale = new Vector3(-1, 1, 1);
                curSpeed = 7;
            }
            else if (trailActive == false)
            {
                trailActive = true;
                transform.localScale = new Vector3(1, 1, 1);
                curSpeed = -7;
            }
            if(transform.position.x > right)
            {
                curSpeed = 0;
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position = new Vector2(right, transform.position.y);
                phaseActive = true;
                trailActive = false;
            }
            if (transform.position.x < left)
            {
                curSpeed = 0;
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = new Vector2(left, transform.position.y);
                phaseActive = true;
                trailActive = false;
            }
            if (firstPhase == false)phase = Random.Range(1, 4);
            curCount = countAttack + Random.Range(0, countRandom);
            reloadJump = Random.Range(1f, 1 + reloadJumpRandom);
        }
        switch (phase)
        {
            case 1:
                {
                    if (time > 0.1f && time <= 0.7f) anim.SetInteger("phase", 1);
                    if(time < 1)
                    {
                        anim.SetBool("walk", false);
                        reloadMoment = 0;
                    }
                    else
                    {
                        anim.SetBool("walk", true);
                        if (transform.localScale.x == 1)
                        {
                            curSpeed = 1;
                            rotate = 0;
                        }
                        else
                        {
                            curSpeed = -1;
                            rotate = 180;
                        }
                        if (transform.position.x > right) transform.localScale = new Vector3(-1, 1, 1);
                        if (transform.position.x < left) transform.localScale = new Vector3(1, 1, 1);
                        if (time >= reloadJump)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce + Random.Range(0, randomJump));
                            reloadJump = time + Random.Range(1f, 1 + reloadJumpRandom);
                        }
                        reloadMoment += Time.deltaTime;
                        if (reloadMoment >= reloadR)
                        {
                            if (curCount > 0)
                            {
                                Instantiate(rocketFire, rocketPos.transform.position, Quaternion.Euler(0, 0, rotate));
                                Instantiate(effectFire, rocketPos.transform.position, Quaternion.Euler(0, 0, rotate));
                                curCount--;
                            }
                            else
                            {
                                firstPhase = false;
                                phaseActive = false;
                            }
                            reloadMoment = 0;
                        }
                    }
                }
                break;
            case 2:
                {
                    if (time > 0.1f && time <= 0.7f) anim.SetInteger("phase", 2);
                    if (time < 1)
                    {
                        anim.SetBool("walk", false);
                        reloadMoment = 0;
                    }
                    else
                    {
                        anim.SetBool("walk", true);
                        if (transform.localScale.x == 1)
                        {
                            curSpeed = 5;
                            rotate = 0;
                        }
                        else
                        {
                            curSpeed = -5;
                            rotate = 180;
                        }
                        if (transform.position.x > right || transform.position.x < left)
                        {
                            if (transform.position.x > right) transform.localScale = new Vector3(-1, 1, 1);
                            if (transform.position.x < left) transform.localScale = new Vector3(1, 1, 1);
                            firstPhase = false;
                            phaseActive = false;
                        }
                    }
                }
                break;
            case 3:
                {
                    if (time > 0.1f && time <= 0.7f) anim.SetInteger("phase", 3);
                    if (time < 1)
                    {
                        anim.SetBool("walk", false);
                        reloadMoment = 0;
                    }
                    else
                    {
                        anim.SetBool("walk", true);
                        if (transform.localScale.x == 1)
                        {
                            curSpeed = 2;
                            rotate = 0;
                        }
                        else
                        {
                            curSpeed = -2;
                            rotate = 180;
                        }
                        if (transform.position.x > right) transform.localScale = new Vector3(-1, 1, 1);
                        if (transform.position.x < left) transform.localScale = new Vector3(1, 1, 1);
                        if (time >= reloadJump)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce + Random.Range(0, randomJump));
                            reloadJump = time + Random.Range(1f, 1 + reloadJumpRandom);
                        }
                        reloadMoment += Time.deltaTime;
                        if (reloadMoment >= reloadP)
                        {
                            if (curCount > 0)
                            {
                                penguin.SetActive(true);
                                curCount--;
                            }
                            else
                            {
                                firstPhase = false;
                                phaseActive = false;
                            }
                            reloadMoment = 0;
                        }
                    }
                }
                break;
        }
        if (reloadMoment >= reloadR - 0.3f && curCount > 0 && phase == 1) warning.SetActive(true);
        else warning.SetActive(false);
        if (transform.position.y > yPos) anim.SetBool("ground", false);
        else anim.SetBool("ground", true);
    }
}
