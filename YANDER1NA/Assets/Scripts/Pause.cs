using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    private bool quit;
    [SerializeField] private GameObject menu;
    [SerializeField] private AudioMixerGroup audioMixer;
    [SerializeField] private GameObject player;
    private Rindik rin;
    private RinYandere yandere;
    private void Start()
    {
        rin = player.GetComponentInChildren<Rindik>();
        yandere = player.GetComponentInChildren<RinYandere>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.JoystickButton7)) PausePress();
    }
    public void PausePress()
    {
        if (menu.activeInHierarchy)
        {
            rin.activeControll = true;
            yandere.enabled = true;
            audioMixer.audioMixer.SetFloat("lowpass", 22000);
            menu.SetActive(false);
        }
        else
        {
            rin.activeControll = false;
            yandere.enabled = false;
            audioMixer.audioMixer.SetFloat("lowpass", 2000);
            menu.SetActive(true);
        }
    }
}
