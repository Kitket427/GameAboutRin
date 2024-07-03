using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    private TrailEffect[] trails;
    [SerializeField] private float time;
    private float timer;
    [SerializeField] private AudioSource sfx;
    private Animator anim;
    void Start()
    {
        trails = GetComponentsInChildren<TrailEffect>();
        foreach (var item in trails)
        {
            item.enabled = false;
        }
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= time)
        {
            sfx.enabled = true;
            foreach (var item in trails)
            {
                item.enabled = true;
            }
            transform.Translate(Vector2.up * Time.deltaTime * 222);
            if (timer >= time + 0.6f) Destroy(gameObject);
            anim.SetBool("ground", false);
        }
        else
        {
            if(GetComponent<Rigidbody2D>().velocity.y < -1) anim.SetBool("ground", false);
            else anim.SetBool("ground", true);
        }
    }
}
