using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private bool floatSpeed;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (floatSpeed == false) anim.speed = speed;
        else anim.SetFloat("speed", speed);
    }
}
