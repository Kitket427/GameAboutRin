using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemySpawn
{
    public GameObject enemy;
    public GameObject enemyBack;
}
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawn[] enemies;
    private Animator anim;
    private int count;
    private Transform player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
    }
    private void Update()
    {
        if (count < enemies.Length)
        {
            if (player.position.x < transform.position.x)
            {
                enemies[count].enemyBack.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                enemies[count].enemyBack.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public void Spawn()
    {
        if (count < enemies.Length)
        {
            enemies[count].enemyBack.SetActive(true);
            anim.SetTrigger("Spawn");
            Invoke(nameof(Enemy), 0.5f);
        }
    }
    void Enemy()
    {
        if (count < enemies.Length)
        {
            Instantiate(enemies[count].enemy, enemies[count].enemyBack.transform.position, Quaternion.identity);
            enemies[count].enemyBack.SetActive(false);
            count++;
        }
    }
}
