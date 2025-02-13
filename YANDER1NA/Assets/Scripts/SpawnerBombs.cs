using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBombs : MonoBehaviour
{
    [SerializeField] private float timeToSpawn, random;
    [SerializeField] private GameObject obj, effect;
    [SerializeField] private bool fix;
    [SerializeField] private bool fixDouble;
    private void Start()
    {
        OnEnable();
    }
    void Spawn()
    {
        if (fix == false) Instantiate(obj, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        else
        {
            Instantiate(obj, transform.position, transform.rotation);
            Instantiate(effect, transform.position, transform.rotation);
        }
        OnEnable();
    }
    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Spawn), timeToSpawn + Random.Range(0, random));
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Update()
    {
        if (fixDouble)
        {
            if (transform.lossyScale.x == 1) transform.rotation = Quaternion.Euler(0, 0, 90);
            else transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
