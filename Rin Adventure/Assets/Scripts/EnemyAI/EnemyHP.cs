using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int hp, maxHp;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Material[] materials;
    private float timer;
    private bool timerWay;
    private AudioSource sfx;
    [SerializeField] private GameObject[] effects;
    [SerializeField] private bool effectRotate;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        if(sprites.Length == 0) sprites = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if(hp <= maxHp*0.44f) 
        {
            if(timerWay)
            {
                timer -= Time.deltaTime * 22f * (0.5f - (float)hp / (float)maxHp);
                if (timer < 0.4f) timerWay = false;
            }
            else
            {
                timer += Time.deltaTime * 22f * (0.5f - (float)hp / (float)maxHp);
                if (timer > 1) timerWay = true;
            }
            foreach (var sprite in sprites)
            {
                sprite.color = new Color(1, timer, timer);
            }
        }
        else
        {
            foreach (var sprite in sprites)
            {
                sprite.color = Color.white;
            }
        }
    }
    void ITakeDamage.TakeDamage(int damage)
    {
        hp -= damage;
        foreach (var sprite in sprites)
        {
            sprite.material = materials[1];
        }
        Invoke(nameof(ReturnMat), 0.03f);
        sfx.Play();
        if(damage < 20) Instantiate(effects[0], transform.position, Quaternion.identity);
        else Instantiate(effects[1], transform.position, Quaternion.identity);
        if(hp <= 0)
        {
            Invoke(nameof(Dead), 0.1f);
        }
    }
    private void ReturnMat()
    {
        foreach (var sprite in sprites)
        {
            sprite.material = materials[0];
        }
    }
    private void Dead()
    {
        CancelInvoke();
        for (int i = 2; i < effects.Length; i++)
        {
            if (effectRotate && transform.localScale.x == -1)
            {
                Instantiate(effects[i], transform.position, Quaternion.Euler(0,180,0));
            }
            else
            {
                Instantiate(effects[i], transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }
}
