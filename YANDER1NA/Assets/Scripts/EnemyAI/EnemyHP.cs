using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Trigger
{
    public GameObject obj;
    public bool active;
}
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
    private int layer = 0;
    [SerializeField] private bool fixedLayer;
    [SerializeField] private Trigger[] triggers;
    [SerializeField] private EnemySpawner[] enemySpawner;
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private GameObject comeback;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        if(sprites.Length == 0) sprites = GetComponentsInChildren<SpriteRenderer>();
        if(fixedLayer == false)layer = Random.Range(30, 30000);
        foreach (var sprite in sprites)
        {
            sprite.sortingOrder += layer;
        }
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
        if(triggers.Length > 0)
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].obj.SetActive(triggers[i].active);
            }
        }
        if(enemySpawner.Length > 0)
        {
            for (int i = 0; i < enemySpawner.Length; i++)
            {
                enemySpawner[i].Spawn();
            }
        }
        if(tanks.Length > 0)
        {
            if(tanks[0].transform.localScale.x == 1) Instantiate(tanks[1], tanks[0].transform.position, Quaternion.Euler(0, 0, 0));
            else Instantiate(tanks[1], tanks[0].transform.position, Quaternion.Euler(0, 180, 0));
            tanks[0].SetActive(false);
        }
        if(comeback)
        {
            comeback.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            Instantiate(comeback, transform.position, Quaternion.identity);
        }
    }
}
