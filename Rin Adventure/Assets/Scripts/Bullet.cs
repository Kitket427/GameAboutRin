using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageObj damageObj = collision.GetComponent<DamageObj>();
        if(damageObj == null ) Destroy(gameObject);
    }
    public void Reverse()
    {
        if (speed > 0) speed = -speed;
        gameObject.layer = 8;
    }
}
