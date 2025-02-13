using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class RinHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int lifes, maxLifes;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Animator anim;
    public bool shield;
    public bool dash;
    [SerializeField]private SpriteRenderer[] sprites;
    [SerializeField] private Material[] materials;
    [SerializeField] private bool blink;
    [SerializeField] private GameObject effect;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private GameObject heartbeat;
    [SerializeField] private AudioSource bonus;
    [SerializeField] private GameObject endEffect;
    [SerializeField] private float speedOst;
    [SerializeField] private bool optimizm;
    [SerializeField] private bool gameOverSystemOff;
    private void Start()
    {
        if (gameOverSystemOff == false)
        {
            lifes = 5 + 2 * (PlayerPrefs.GetInt("GameOver") / 3);
            Debug.Log("GameOvers " + PlayerPrefs.GetInt("GameOver"));
        }
        if (lifes > 15) lifes = 15;
        maxLifes = lifes;
        UIactive();
        foreach (var sprite in sprites)
        {
            sprite.material = materials[0];
        }
    }
    void Update()
    {
        if (speedOst != 1)
        {
            if (Time.timeScale < 1 && Time.timeScale != 0)
            {
                Time.timeScale += Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        if (Time.timeScale == 1 && optimizm == false)
        {
            mixerGroup.audioMixer.SetFloat("gameSpeed", 1);
            optimizm = true;
        }
        if (Time.timeScale != 1 && optimizm == true)
        {
            optimizm = false;
        }
        if (optimizm == false) mixerGroup.audioMixer.SetFloat("gameSpeed", Time.timeScale);
        if (speedOst == 1) Time.timeScale = 1;
        if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Time.timeScale = 1f;
            shield = false;
            dash = false;
            lifes = 1;
            gameObject.GetComponent<ITakeDamage>().TakeDamage(1);
        }
    }
    private void UIactive()
    {
        hearts[0].fillAmount = lifes * 1f / 20f;
        hearts[1].fillAmount = maxLifes * 1f / 20f;
        anim.speed = maxLifes * 1f / lifes * 1f;
        if (lifes == 1) heartbeat.SetActive(true);
        else heartbeat.SetActive(false);
    }
    void ITakeDamage.TakeDamage(int damage)
    {
        GetComponent<Rindik>().timeDeactive = 0;
        if (shield == false && dash == false)
        {
            if (damage < 49) lifes -= 1;
            else if (damage < 99) lifes -= 2;
            else if (lifes > 1) lifes = 1;
            else lifes = 0;
            if (lifes > 0) Time.timeScale = 0.1f;
            UIactive();
            InvokeRepeating(nameof(Blink), 0, 0.02f);
            Invoke(nameof(BlinkStop), 2f);
            Instantiate(effect, transform.position, Quaternion.identity);
            shield = true;
            if (lifes <= 0)
            {
                if (gameOverSystemOff == false)
                {
                    PlayerPrefs.SetInt("GameOver", (PlayerPrefs.GetInt("GameOver") + 1));
                    PlayerPrefs.SetInt("Death", (PlayerPrefs.GetInt("Death") + 1));
                }
                for (int i = 0; i < 4; i++) Instantiate(effect, transform.position, Quaternion.identity);
                if(endEffect) Instantiate(endEffect, transform.position, Quaternion.identity);
                gameOver.Game();
                gameObject.SetActive(false);
            }
        }
    }
    void Blink()
    {
        if(blink)
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[0];
            }
            blink = false;
        }
        else
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[1];
            }
            blink = true;
        }
    }
    void BlinkStop()
    {
        CancelInvoke();
        shield = false;
        foreach (var sprite in sprites)
        {
            sprite.material = materials[0];
        }
        blink = false;
        if (lifes == 1) BlinkOneHit();
    }
    void BlinkOneHit()
    {
        if (blink)
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[0];
            }
            blink = false;
            if(lifes == 1)Invoke(nameof(BlinkOneHit), 0.7f);
        }
        else
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[2];
            }
            blink = true;
            Invoke(nameof(BlinkOneHit), 0.1f);
        }
    }
    public void Heal(int hearts)
    {
        lifes += hearts;
        if (lifes > maxLifes) lifes = maxLifes;
        UIactive();
        bonus.Play();
    }
}
