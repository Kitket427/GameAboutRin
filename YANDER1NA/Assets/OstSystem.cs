using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstSystem : MonoBehaviour
{
    private Animator anim;
    private float time = 8;
    [SerializeField] private Alert[] alerts;
    [SerializeField] private GameObject alertSFX;
    [SerializeField] private bool alwaysActive;
    private void Start()
    {
        anim = GetComponent<Animator>();
        alerts = FindObjectsOfType<Alert>();
    }
    public void Battle()
    {
        time = 0;
        foreach (var alert in alerts)
        {
            alert.On();
        }
        alertSFX.SetActive(true);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 7)
        {
            if(alwaysActive == false) anim.SetBool("battle", false);
            if(time < 7.5)
            {
                foreach (var alert in alerts)
                {
                    if(alert.alertLights == false || alwaysActive == false)alert.Off();
                }
                if (alwaysActive == false) alertSFX.SetActive(false);
            }
        }
        else
        {
            anim.SetBool("battle", true);
        }
    }
}
