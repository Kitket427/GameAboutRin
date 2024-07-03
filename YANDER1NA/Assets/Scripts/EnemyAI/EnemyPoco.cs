using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoco : MonoBehaviour
{
    [SerializeField] private float maxDistance = 130;
    private Transform target;
    [SerializeField] private float time, timeChill;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject[] warnings;
    private OstSystem ost;
    private bool active;
    [SerializeField] private GameObject rocket;
    private bool rocketLaunch;
    [SerializeField] private EnemySpawner[] enemySpawner;
    private void Start()
    {
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
        time = 0;
    }
    private void Update()
    {
        if(active)time += Time.deltaTime;
        if (Vector2.Distance(transform.position, target.position) < maxDistance) active = true;
        if (time >= timeChill + 0.4f)
        {
            foreach (var warning in warnings)
            {
                warning.SetActive(true);
            }
            if (ost) ost.Battle();
        }
        if(time >= timeChill + 1f && rocketLaunch == false)
        {
            rocket.transform.localScale = new Vector3(1, transform.localScale.x, 1);
            Instantiate(rocket, pos.position, Quaternion.Euler(0, 0, 90));
            rocketLaunch = true;
            if (enemySpawner.Length > 0)
            {
                for (int i = 0; i < enemySpawner.Length; i++)
                {
                    enemySpawner[i].Spawn();
                }
            }
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        time = 0;
    }
}
