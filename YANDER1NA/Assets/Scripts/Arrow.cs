using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private bool active;
    private Animator anim;
    private void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("active", active);
    }
}
