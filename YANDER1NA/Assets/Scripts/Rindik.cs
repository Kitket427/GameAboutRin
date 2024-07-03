using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.UI;
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
    private bool dash;
    private TrailEffect[] trails;
    private bool dashReady = true;
    private RinHealth rinShield;
    [SerializeField] private AudioSource dashSFX;
    public bool activeControll;

    [SerializeField] private float speedAttack, timePower, timePowerUp;
    [SerializeField] private Image rinHair;

    [SerializeField] private AudioSource bonus;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
        anim = GetComponentsInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(timeDeactive < 16) ostSpeed = 1;
        else ostSpeed = 0.3f;
        trails = GetComponentsInChildren<TrailEffect>();
        foreach (var item in trails)
        {
            item.enabled = false;
        }
        rinShield = GetComponent<RinHealth>();
        //activeControll = true;
        anim[1].speed = speedAttack;
        anim[1].GetComponent<AudioSource>().pitch = speedAttack;
    }
    private void FixedUpdate()
    {
        if(dash == false) rb.velocity = new Vector2(movespeed * speed, rb.velocity.y);
    }
    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) == false || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) == false || Input.GetAxisRaw("JoyX") < 0) && activeControll)
        {
            movespeed = -1;
            timeDeactive = 0;
            start = true;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) || Input.GetAxisRaw("JoyX") > 0) && activeControll)
        {
            movespeed = 1;
            timeDeactive = 0;
            start = true;
        }
        else movespeed = 0;
        if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5)) && dashReady && activeControll)
        {
            rb.velocity = new Vector2(transform.localScale.x * 300, 0);
            dash = true;
            dashReady = false;
            rinShield.dash = true;
            foreach (var item in trails)
            {
                item.enabled = true;
            }
            Invoke(nameof(DashOff), 0.15f);
            Invoke(nameof(DashReady), 0.7f);
            rb.gravityScale = 0;
            timeDeactive = 0;
            dashSFX.enabled = true;
            start = true;
        }
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
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && isGround && activeControll)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 10);
            timeDeactive = 0;
            start = true;
            jump = true;
            sfx.Play();
        }
        if ((Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton0)) && rb.velocity.y > 0 && jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            jump = false;
        }
        if(rb.velocity.y < -75) rb.velocity = new Vector2(rb.velocity.x, -75);

        if (rb.velocity.y > 1) eyes.localPosition = new Vector2(0.5f, 0f);
        else if(start) eyes.localPosition = new Vector2(0.5f, -0.5f);

        if ((Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.JoystickButton2)) && activeControll)
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
            else
            {
                if (timePower > 6) ostSpeed = 1.1f;
                else if (timePower > 0) ostSpeed = 1f * (10 + timePower / 6) / 10;
                else ostSpeed = 1f;
            }
        }
        if (activeControll == false)
        {
            timeDeactive = 0;
        }
        mixerGroup.audioMixer.SetFloat("ostSpeed", ostSpeed);

        if(timePower <= 0)
        {
            rinHair.fillAmount = 0;
            anim[1].speed = speedAttack;
            anim[1].GetComponent<AudioSource>().pitch = speedAttack;
        }
        else
        {
            rinHair.fillAmount = timePower / 6f;
            timePower -= Time.deltaTime;
            if (timePower >= 6)
            {
                anim[1].speed = speedAttack * 1.6f;
                anim[1].GetComponent<AudioSource>().pitch = speedAttack * 1.6f;
            }
            else
            {
                anim[1].speed = speedAttack * (10+timePower)/10;
                anim[1].GetComponent<AudioSource>().pitch = speedAttack * (10 + timePower) / 10;
            }
            timeDeactive = 0;
        }
        if(timePowerUp > 0 && timePower <= 24)
        {
            timePowerUp -= Time.deltaTime;
            timePower += Time.deltaTime * 4;
        }
        else
        {
            timePowerUp = 0;
        }
    }
    void DashOff()
    {
        dash = false;
        foreach (var item in trails)
        {
            item.enabled = false;
        }
        rinShield.dash = false;
        rb.gravityScale = 24;
        dashSFX.enabled = false;
    }
    void DashReady()
    {
        dashReady = true;
    }
    public void Power(int powerTimeUp)
    {
        timePowerUp = powerTimeUp;
        bonus.Play();
    }
}
