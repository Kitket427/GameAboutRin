using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool attack, reload;
    [SerializeField] private float time;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHP enemyHP = collision.GetComponent<EnemyHP>();
        if(enemyHP != null && attack && reload == false)
        {
            enemyHP.TakeDamage(damage);
            reload = true;
            Invoke(nameof(Reload), time);
         }
    }
    void Reload()
    {
        reload = false;
    }
}
