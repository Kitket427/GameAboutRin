using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    private void Update()
    {
        if (lights[0].activeInHierarchy)
        {
            transform.Rotate(0,0,Time.deltaTime * 400f);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }
    public void On()
    {
        lights[0].SetActive(true);
        lights[1].SetActive(true);
    }
    public void Off()
    {
        lights[0].SetActive(false);
        lights[1].SetActive(false);
    }
}
