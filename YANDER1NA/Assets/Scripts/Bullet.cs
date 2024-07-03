using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] private bool col;
    [SerializeField] private GameObject deadEffect;
    [SerializeField] private float deadTime;
    private void Start()
    {
        Invoke(nameof(DestroyBullet), deadTime);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageObj damageObj = collision.GetComponent<DamageObj>();
        if (damageObj == null || col) DestroyBullet();
    }
    public void Reverse()
    {
        if (speed > 0) speed = -speed;
        gameObject.layer = 8;
    }
    private void DestroyBullet()
    {
        Instantiate(deadEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
