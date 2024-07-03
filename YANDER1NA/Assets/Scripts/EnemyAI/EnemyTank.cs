using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    private Transform target;
    [SerializeField] private float speed, left, right, timeToCheck, time, timeChill, reload, currentCount, count, maxDistance;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject warning;
    private OstSystem ost;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * transform.localScale.x, rb.velocity.y);
    }
    private void Update()
    {
        if(transform.position.x > right)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            EnemyDown();
        }
        if (transform.position.x < left)
        {
            transform.localScale = new Vector3(1, 1, 1);
            EnemyDown();
        }
        if(transform.localScale.x == 1 && target.position.x < transform.position.x || transform.localScale.x == -1 && transform.position.x < target.position.x)
        {
            timeToCheck += Time.deltaTime;
            time = 0;
            currentCount = 0;
            if (timeToCheck >= 0.5f) EnemyUp();
        }
        else
        {
            if (timeToCheck > 0) EnemyDown();
            timeToCheck = 0;
            time += Time.deltaTime;
        }
        if (time > timeChill + 1 && Vector2.Distance(transform.position, target.position) < maxDistance)
        {
            foreach (var item in bullets)
            {
                if(transform.localScale.x == 1) Instantiate(item, pos.position, Quaternion.Euler(0, 0, 0));
                else Instantiate(item, pos.position, Quaternion.Euler(0, 0, 180));
            }
            anim.SetTrigger("fire");
            currentCount--;
            time = timeChill + 1 - reload;
        }
        if (Vector2.Distance(transform.position, target.position) > maxDistance) time = timeChill;
        if (currentCount <= 0)
        {
            currentCount = count;
            time = 0;
        }
        if (time > timeChill + 0.4f)
        {
            warning.SetActive(true);
            if (ost) ost.Battle();
        }
        else
        {
            warning.SetActive(false);
        }
        enemy.transform.localPosition = new Vector2(0, 10);
    }
    void EnemyDown()
    {
        if(transform.position.x < right && transform.position.x > left) transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        enemy.SetActive(false);
        time = timeChill;
        warning.SetActive(false);
        speed = 33;
        CancelInvoke();
    }
    void EnemyUp()
    {
        enemy.SetActive(true);
        timeToCheck = 0;
        speed = 66;
    }
}
