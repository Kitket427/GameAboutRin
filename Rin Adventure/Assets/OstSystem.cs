using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstSystem : MonoBehaviour
{
    private Animator anim;
    private float time = 8;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Battle()
    {
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 7)
        {
            anim.SetBool("battle", false);
        }
        else
        {
            anim.SetBool("battle", true);
        }
        
    }
}
