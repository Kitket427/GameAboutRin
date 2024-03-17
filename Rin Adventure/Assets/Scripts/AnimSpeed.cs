using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float speed;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = speed;
    }
}
