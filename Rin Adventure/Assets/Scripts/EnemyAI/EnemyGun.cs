using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Transform target;
    [SerializeField] private float speed, animSpeed, currentSpeed, time, timeChill, reload;
    [SerializeField] private int count, currentCount;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject warning;
    private float rotateZ;
    [SerializeField] private Transform gun;
    private Animator animGun;
    private float randomDistance;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Rindik>().GetComponent<Transform>();
        anim.speed = speed * 0.03f;
        anim.SetBool("minus", true);
        animGun = gun.GetComponentInChildren<Animator>();
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
            if (Vector2.Distance(transform.position, target.position) < randomDistance)
            {
                currentSpeed = -1;
                anim.SetBool("minus", true);
            }
            else if(Vector2.Distance(transform.position, target.position) > randomDistance+22)
            {
                currentSpeed = 1;
                anim.SetBool("minus", false);
            }
        }
        else
        {
            currentSpeed = 0;
            randomDistance = Random.Range(33f, 66f);
        }
        if(rb.velocity.x > -0.1f && rb.velocity.x < 0.1f) anim.SetBool("move", false);
        else anim.SetBool("move", true);
        if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gun.rotation = Quaternion.Euler(180, 180, rotateZ);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            gun.rotation = Quaternion.Euler(0, 0, rotateZ);
        }
        if (rb.velocity.y > 1 || rb.velocity.y < -1) anim.SetBool("ground", false);
        else anim.SetBool("ground", true);
        if (time > timeChill + 1 && Vector2.Distance(transform.position, target.position) < 135)
        {
            foreach (var item in bullets)
            {
                Instantiate(item, pos.position, Quaternion.Euler(0, 0, rotateZ));
            }
            animGun.SetTrigger("fire");
            currentCount--;
            time = timeChill + 1 - reload;
        }
        if (Vector2.Distance(transform.position, target.position) > 130) time = timeChill;
        if (currentCount <= 0)
        {
            currentCount = count;
            time = 0;
        }
        if(time > timeChill + 0.4f)
        {
            warning.SetActive(true);
        }
        else
        { 
            warning.SetActive(false); 
        }
    }
}