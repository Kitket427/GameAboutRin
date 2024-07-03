using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKit1 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Transform target;
    [SerializeField] private float speed, animSpeed, currentSpeed, time, timeChill, reload;
    [SerializeField] private int count, currentCount;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform[] pos;
    [SerializeField] private GameObject warning;
    private float rotateZ;
    [SerializeField] private Transform[] gun;
    [SerializeField] private Animator[] animGun;
    private OstSystem ost;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Rindik>().GetComponent<Transform>();
        anim.speed = speed * 0.03f;
        anim.SetBool("minus", true);
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * currentSpeed * transform.localScale.x, rb.velocity.y);
    }
    private void Update()
    {
        Vector3 difference = target.position - transform.position;
        rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        time += Time.deltaTime;
        if (time < timeChill)
        {
            if (Vector2.Distance(transform.position, target.position) < 9 / reload)
            {
                currentSpeed = -1;
                anim.SetBool("minus", true);
            }
            else if(Vector2.Distance(transform.position, target.position) > 10 / reload)
            {
                currentSpeed = 1;
                anim.SetBool("minus", false);
            }
        }
        else
        {
            currentSpeed = 0;
        }
        if (rb.velocity.x > -0.1f && rb.velocity.x < 0.1f) anim.SetBool("move", false);
        else anim.SetBool("move", true);
        if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gun[0].rotation = Quaternion.Euler(180, 180, rotateZ);
            gun[1].rotation = Quaternion.Euler(180, 180, rotateZ);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            gun[0].rotation = Quaternion.Euler(0, 0, rotateZ);
            gun[1].rotation = Quaternion.Euler(0, 0, rotateZ);
        }
        if (rb.velocity.y > 1 || rb.velocity.y < -1) anim.SetBool("ground", false);
        else anim.SetBool("ground", true);
        if (time > timeChill + 1)
        {
            foreach (var item in bullets)
            {
                Instantiate(item, pos[0].position, Quaternion.Euler(0, 0, rotateZ));
                Instantiate(item, pos[1].position, Quaternion.Euler(0, 0, rotateZ));
            }
            animGun[0].SetTrigger("fire");
            animGun[1].SetTrigger("fire");
            currentCount--;
            time = timeChill + 1 - reload;
        }
        if (currentCount <= 0)
        {
            time = 0;
            timeChill = Random.Range(1f, 2f);
            if (reload > 0.11f) reload -= 0.1f;
            else reload = 0.3f;
            currentCount = count + (int)(reload * 10);
            rb.velocity = new Vector2(rb.velocity.x, 111);
            bullets[0].GetComponent<Bullet>().speed = 30 / reload;

        }
        if (time > timeChill + 0.4f)
        {
            warning.SetActive(true);
            ost.Battle();
        }
        else
        {
            warning.SetActive(false);
        }
    }
}