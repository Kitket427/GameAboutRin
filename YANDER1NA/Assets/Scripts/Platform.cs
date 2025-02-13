using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform posPl;
    [SerializeField] private float time, extraTimeDown, speed, posUp, posDown;
    [SerializeField] private float currentime;
    [SerializeField] private bool posUpNow;
    private Rigidbody2D rb;
    [SerializeField] private GameObject[] sfx;
    [SerializeField] private Transform enemy;
    private float posYenemy;
    [SerializeField] private bool deactive, playerOnLift;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemy) posYenemy = enemy.transform.localPosition.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(posPl != null)
        {
            player.parent = transform;
            Debug.Log("Platform");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (posPl != null)
        {
            player.parent = null;
            Debug.Log("none");
        }
    }
    private void FixedUpdate()
    {
        if (deactive == false)
        {
            if (currentime >= time && posUpNow || currentime >= time + extraTimeDown)
            {
                if (posUpNow) rb.velocity = new Vector2(0, -speed);
                else rb.velocity = new Vector2(0, speed);
                sfx[0].SetActive(true);
                sfx[1].SetActive(false);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                sfx[0].SetActive(false);
                rb.velocity = new Vector2(0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
    private void Update()
    {
        if (deactive == false)
        {
            if (enemy) enemy.localPosition = new Vector2(0, posYenemy);
            currentime += Time.deltaTime;
            if (transform.position.y > posUp)
            {
                currentime = 0;
                posUpNow = true;
                transform.position = new Vector2(transform.position.x, posUp);
                sfx[1].SetActive(true);
            }
            if (transform.position.y < posDown)
            {
                currentime = 0;
                posUpNow = false;
                transform.position = new Vector2(transform.position.x, posDown);
                sfx[1].SetActive(true);
            }
        }
        if(playerOnLift) player.parent = transform;
    }
}
