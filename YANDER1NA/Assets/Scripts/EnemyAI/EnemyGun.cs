using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private float minDistance = 30, minDistancePlus = 60, maxDistance = 130;
    private Animator anim;
    private Rigidbody2D rb;
    private Transform target;
    [SerializeField] private float speed, animSpeed, currentSpeed, time, timeChill, timeOnEnable, reload, jumpHowManyInTen, jumpForce;
    [SerializeField] private int count, currentCount;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject warning;
    private float rotateZ;
    [SerializeField] private Transform gun;
    private Animator animGun;
    private float randomDistance;
    private OstSystem ost;
    [SerializeField] private bool notAim;
    [SerializeField] private bool notStop;
    [SerializeField] private bool inTank;
    [SerializeField] private bool groundForever;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        anim.speed = speed * 0.03f;
        if (speed == 0) anim.speed = 1;
        anim.SetBool("minus", true);
        animGun = gun.GetComponentInChildren<Animator>();
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
        time = timeOnEnable;
        currentCount = count;
    }
    private void FixedUpdate()
    {
        if(inTank == false) rb.velocity = new Vector2(speed * currentSpeed * transform.localScale.x, rb.velocity.y);
    }
    private void Update()
    {
        Vector3 difference = target.position - transform.position;
        if(notAim == false) rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        else
        {
            if (transform.localScale.x == 1) rotateZ = 0;
            else rotateZ = 180;
        }
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
            randomDistance = Random.Range(minDistance, minDistancePlus);
        }
        if(rb.velocity.x > -0.1f && rb.velocity.x < 0.1f) anim.SetBool("move", false);
        else anim.SetBool("move", true);
        if (rb.velocity.y < -75) rb.velocity = new Vector2(rb.velocity.x, -75);
        if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gun.rotation = Quaternion.Euler(180, 180, rotateZ);
        }
        else
        {
            if(inTank == false) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(-1, 1, 1);
            gun.rotation = Quaternion.Euler(0, 0, rotateZ);
        }
        if ((rb.velocity.y > 0.2f || rb.velocity.y < -0.2f) && groundForever == false) anim.SetBool("ground", false);
        else anim.SetBool("ground", true);
        if (time > timeChill + 1 && (Vector2.Distance(transform.position, target.position) < maxDistance + 5 || notStop))
        {
            foreach (var item in bullets)
            {
                Instantiate(item, pos.position, Quaternion.Euler(0, 0, rotateZ));
            }
            animGun.SetTrigger("fire");
            currentCount--;
            time = timeChill + 1 - reload;
        }
        if (Vector2.Distance(transform.position, target.position) > maxDistance && notStop == false) time = timeChill;
        if (currentCount <= 0)
        {
            if (Random.Range(1, 11) < jumpHowManyInTen) rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            currentCount = count;
            time = 0;
        }
        if (time > timeChill + 0.4f)
        {
            warning.SetActive(true);
            if(ost)ost.Battle();
        }
        else
        { 
            warning.SetActive(false); 
        }
    }
    private void OnDisable()
    {
        time = timeOnEnable;
        currentCount = count;
        warning.SetActive(false);
    }
}