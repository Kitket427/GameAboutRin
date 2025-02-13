using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float time;
    private int currentCount;
    [SerializeField] private float reload, randomReload, rate, distance;
    [SerializeField] private int count;
    public bool active;
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private GameObject warning;
    private Transform target;
    private OstSystem ost;
    private void Start()
    {
        time = 0 - Random.Range(0, randomReload);
        currentCount = count;
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
    }
    private void OnEnable()
    {
        time = 0 - Random.Range(0, randomReload);
        currentCount = count;
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        if (FindObjectOfType<OstSystem>()) ost = FindObjectOfType<OstSystem>();
    }
    private void Update()
    {
        if(active && Vector2.Distance(transform.position, target.position) < distance)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0 - Random.Range(0, randomReload);
        }
        if(time >= reload)
        {
            warning.SetActive(true);
            if(time >= reload + 0.2f + rate && currentCount > 0)
            {
                foreach (var pos in spawnPos)
                {
                    foreach (var item in spawns)
                    {
                        Instantiate(item, pos.position, transform.rotation);
                    }
                }
                time = reload + 0.2f;
                currentCount--;
            }
            if (currentCount <= 0) time = 0 - Random.Range(0, randomReload);
            if (ost) ost.Battle();
        }
        else
        {
            warning.SetActive(false);
            currentCount = count;
        }
    }
}
