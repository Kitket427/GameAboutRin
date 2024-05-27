using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;

public class Rindik : MonoBehaviour
{
    private Animator[] anim;
    private Rigidbody2D rb;
    [SerializeField] private float speed, movespeed, jumpForce;
    public float timeDeactive;
    [SerializeField] private LayerMask layer;
    [SerializeField] private LayerMask enLayer;
    [SerializeField] private Transform posJump;
    [SerializeField] private Vector2 hitboxJump;
    private bool isGround;
    private bool isEnemy;
    [SerializeField] private CinemachineVirtualCamera prioIdle;
    [SerializeField] private CinemachineBrain brainIdle;
    [SerializeField] private GameObject[] faces;
    [SerializeField] private Transform eyes;
    private bool start, jump;
    private AudioSource sfx;
    [SerializeField] private AudioMixerGroup mixerGroup;
    private float ostSpeed;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        anim = GetComponentsInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(timeDeactive < 16) ostSpeed = 1;
        else ostSpeed = 0.3f;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movespeed * speed, rb.velocity.y);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false)
        {
            movespeed = -1;
            timeDeactive = 0;
            start = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow))
        {
            movespeed = 1;
            timeDeactive = 0;
            start = true;
        }
        else movespeed = 0;
        if (rb.velocity.x > -1f && rb.velocity.x < 1f) anim[0].SetBool("move", false);
        else anim[0].SetBool("move", true);

        isGround = Physics2D.OverlapBox(posJump.position, hitboxJump, 0, layer);
        anim[0].SetBool("ground", isGround);
        isEnemy = Physics2D.OverlapBox(posJump.position, hitboxJump, 0, enLayer);
        if (isEnemy && isGround == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 7);
            timeDeactive = 0;
            start = true;
            anim[0].SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.Z) && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 10);
            timeDeactive = 0;
            start = true;
            jump = true;
            sfx.Play();
        }
        if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0 && jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            jump = false;
        }
        if(rb.velocity.y < -75) rb.velocity = new Vector2(rb.velocity.x, -75);

        if (rb.velocity.y > 1) eyes.localPosition = new Vector2(0.5f, 0f);
        else if(start) eyes.localPosition = new Vector2(0.5f, -0.5f);

        if (Input.GetKey(KeyCode.X))
        {
            anim[1].SetBool("attack", true);
            timeDeactive = 0;
            start = true;
        }
        else
        {
            anim[1].SetBool("attack", false);
        }
        if (timeDeactive > 16)
        {
            faces[0].SetActive(true);
            faces[1].SetActive(false);
        }
        else if (timeDeactive > 10)
        {
            prioIdle.Priority = 99;
            brainIdle.m_DefaultBlend.m_Time = 9;
        }
        else
        {
            prioIdle.Priority = 0;
            faces[1].SetActive(true);
            faces[0].SetActive(false);
            brainIdle.m_DefaultBlend.m_Time = 1.5f;
        }
        timeDeactive += Time.deltaTime;
        if(timeDeactive > 10)
        {
            if (ostSpeed > 0.4f) ostSpeed -= Time.deltaTime / 20f;
            else ostSpeed = 0.4f;
        }
        else
        {
            if (ostSpeed < 1) ostSpeed += Time.deltaTime;
            else ostSpeed = 1f;
        }
        mixerGroup.audioMixer.SetFloat("ostSpeed", ostSpeed);
    }
}
