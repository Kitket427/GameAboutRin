using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool attack, reload, reverse;
    [SerializeField] private float time;
    [SerializeField] private Collider2D[] colliders;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamage damageObj = collision.GetComponent<ITakeDamage>();
        if (damageObj != null && attack && reload == false)
        {
            damageObj.TakeDamage(damage);
            reload = true;
            if (colliders.Length > 0)
            {
                foreach (var item in colliders)
                {
                    item.enabled = false;
                }
            }
            Invoke(nameof(Reload), time);
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if(bullet != null && reverse)
        {
            bullet.Reverse();
        }
        if(bullet == null && damageObj == null) Destroy(bullet);
    }
    void Reload()
    {
        reload = false;
        if (colliders.Length > 0)
        {
            foreach (var item in colliders)
            {
                item.enabled = true;
            }
        }
    }
}
