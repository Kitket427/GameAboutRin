using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinYandere : MonoBehaviour
{
    [SerializeField] private bool attack;
    private AudioSource sfx;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (attack == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) == false || Input.GetAxisRaw("JoyX") < 0)
            {
                transform.parent.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) || Input.GetAxisRaw("JoyX") > 0)
            {
                transform.parent.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public void Attack()
    {
        sfx.Play();
    }
}
