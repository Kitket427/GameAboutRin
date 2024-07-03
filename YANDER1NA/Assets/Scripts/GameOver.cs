using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup audioMixer;
    [SerializeField] private Image screen;
    private Animator anim;
    [SerializeField] private int restartScene;
    private bool active;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject[] ungameObjects;
    private float time;
    [SerializeField] private Pause pause;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Game()
    {
        foreach (var item in gameObjects)
        {
            item.SetActive(true);
        }
        foreach (var item in ungameObjects)
        {
            item.SetActive(false);
        }
        active = true;
    }
    private void Update()
    {
        if (active)
        {
            audioMixer.audioMixer.SetFloat("gameSpeed", Time.timeScale);
            if (Time.timeScale > 0.02f) Time.timeScale -= Time.unscaledDeltaTime / 8;
            else Time.timeScale = 0f;
            anim.SetTrigger("active");
            time += Time.unscaledDeltaTime;
            pause.enabled = false;
            audioMixer.audioMixer.SetFloat("lowpass", 22000);
            if (time >= 8) Restart();
        }
    }
    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(restartScene);
    }
}
