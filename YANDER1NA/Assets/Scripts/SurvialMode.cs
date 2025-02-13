using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvialMode : MonoBehaviour
{
    [SerializeField] private int points, level;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private RinHealth rinHealth;
    private void Start()
    {
        BattleSpawner(0);
    }
    void BattleSpawner(int pointReady)
    {
        if (points == 0) Invoke(nameof(NextWave), 7f);
        else if (pointReady > points || pointReady != 1 && pointReady != 3 && pointReady != 12 && pointReady != 9 && pointReady != 15 && pointReady != 18 && pointReady != 24) BattleSpawner(pointReady - 1);
        else
        {
            Spawner(pointReady);
            points-= pointReady;
        }
    }
    void Spawner(int point)
    {
        switch (point)
        {
            case 1:
                if (level < 5) Instantiate(enemies[0], new Vector2(Random.Range(-240f, 240f), 80), transform.rotation);
                else if (level < 10) Instantiate(enemies[Random.Range(0, 2)], new Vector2(Random.Range(-240f, 240f), 80), transform.rotation);
                else Instantiate(enemies[Random.Range(0,3)], new Vector2(Random.Range(-240f, 240f), 80), transform.rotation);
                Invoke(nameof(Repeat), 1.7f);
                break;
            case 3:
                if (Random.Range(0, 2) == 0 || level < 15) Instantiate(enemies[3], new Vector2(Random.Range(-240f, 240f), 80), transform.rotation);
                else Instantiate(enemies[10], new Vector2(Random.Range(-240f, 240f), 80), transform.rotation);
                Invoke(nameof(Repeat), 2.7f);
                break;
            case 12:
                Instantiate(enemies[11], new Vector2(Random.Range(-240f, 240f), 80), transform.rotation);
                Invoke(nameof(Repeat), 7.7f);
                break;
            case 9:
                if(Random.Range(0,2) == 0)Instantiate(enemies[4], new Vector2(-280, -8), enemies[4].transform.rotation);
                else Instantiate(enemies[5], new Vector2(280, -8), enemies[5].transform.rotation);
                Invoke(nameof(Repeat), 4.7f);
                break;
            case 15:
                if (Random.Range(0, 2) == 0) Instantiate(enemies[6], new Vector2(-300, Random.Range(30f,50f)), enemies[6].transform.rotation);
                else Instantiate(enemies[6], new Vector2(300, Random.Range(30f, 50f)), enemies[6].transform.rotation);
                Invoke(nameof(Repeat), 7.7f);
                break;
            case 18:
                if (Random.Range(0, 2) == 0) Instantiate(enemies[12], new Vector2(-280, -8), enemies[4].transform.rotation);
                else Instantiate(enemies[12], new Vector2(280, -8), enemies[5].transform.rotation);
                Invoke(nameof(Repeat), 12.7f);
                break;
            case 24:
                if (Random.Range(0, 2) == 0) Instantiate(enemies[13], new Vector2(-300, Random.Range(30f, 50f)), enemies[6].transform.rotation);
                else Instantiate(enemies[13], new Vector2(300, Random.Range(30f, 50f)), enemies[6].transform.rotation);
                Invoke(nameof(Repeat), 17.7f);
                break;
        }
    }
    void Repeat()
    {
        if(level >= 25) BattleSpawner(Random.Range(1, 25));
        else BattleSpawner(Random.Range(1, level));
    }
    void NextWave()
    {
        EndBattle();
    }
    void EndBattle()
    {
        points = level + 1;
        level++;
        for(int i = 0, k = 1; i < points; i++)
        {
            if(i > k*7)
            {
                level++;
                k++;
            }
        }
        if(level >= 10)
        {
            rinHealth.Heal(level / 9);
        }
        if (level >= 11) enemies[7].SetActive(true);
        if (level >= 27) enemies[8].SetActive(true);
        if (level >= 47) enemies[9].SetActive(true);
        Repeat();
    }
}
