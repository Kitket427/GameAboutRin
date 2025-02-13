using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AslonGun : MonoBehaviour
{
    private Animator anim;
    private float time, reload;
    [SerializeField] private GameObject[] fire;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyHP enemyHP = collision.GetComponent<EnemyHP>();
        if(enemyHP != null)
        {
            time = 1;
        }
    }
    private void Update()
    {
        if(time > 0)
        {
            reload += Time.deltaTime;
            time -= Time.deltaTime;
            if(reload >= 0.13f)
            {
                Instantiate(fire[0], fire[2].transform.position, Quaternion.Euler(0, 0, 90 + (90 * transform.lossyScale.x) + Random.Range(-22f, 22f)));
                Instantiate(fire[1], fire[2].transform.position, Quaternion.Euler(0, 0, 90 + (90 * transform.lossyScale.x)));
                anim.SetTrigger("fire");
                reload = 0;
            }
        }
        else
        {
            reload = 0;
        }
    }
}
