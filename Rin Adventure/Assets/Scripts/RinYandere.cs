using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinYandere : MonoBehaviour
{
    [SerializeField] private bool attack;
    private AudioSource sfx;
    [SerializeField] private Transform text;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (attack == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false)
            {
                transform.parent.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow))
            {
                transform.parent.localScale = new Vector3(1, 1, 1);
            }
        }
        text.transform.localScale = new Vector3(transform.parent.localScale.x/5, transform.parent.localScale.y / 5, transform.parent.localScale.z / 5); ;
    }
    public void Attack()
    {
        sfx.Play();
    }
}
