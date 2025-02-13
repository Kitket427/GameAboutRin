using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfx;
    private AudioSource audioSource;
    private GameObject button;
    [SerializeField] private Slider[] sliders;
    private float[] sliderCount;
    [SerializeField] private AudioMixerGroup audioMixer;
    private bool active;
    [SerializeField] private GameObject pause;
    private float volume;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume = audioSource.volume;
        audioSource.volume = 0;
        button = EventSystem.current.currentSelectedGameObject;
        sliderCount = new float[2];
        if (PlayerPrefs.HasKey("ost") == false)
        {
            sliderCount[0] = sliders[0].value;
            sliderCount[1] = sliders[1].value;
            PlayerPrefs.SetFloat("sfx", sliderCount[0]);
            PlayerPrefs.SetFloat("ost", sliderCount[1]);
        }
        else
        {
            sliders[0].value = PlayerPrefs.GetFloat("sfx");
            sliders[1].value = PlayerPrefs.GetFloat("ost");
            sliderCount[0] = sliders[0].value;
            sliderCount[1] = sliders[1].value;
        }
        if (sliderCount[0] > 0.02f) audioMixer.audioMixer.SetFloat("sfx", -20 + 20 * sliderCount[0]);
        else audioMixer.audioMixer.SetFloat("sfx", -80);
        if (sliderCount[1] > 0.02f) audioMixer.audioMixer.SetFloat("ost", -20 + 20 * sliderCount[1]);
        else audioMixer.audioMixer.SetFloat("ost", -80);

        Invoke(nameof(Active), 0.1f);
        Cursor.visible = false;
        Invoke(nameof(SFX), 0.6f);
    }
    void SFX()
    {
        audioSource.volume = volume;
    }
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != button && Input.GetKeyDown(KeyCode.Return) == false && Input.GetKeyDown(KeyCode.JoystickButton0) == false && active)
        {
            audioSource.clip = sfx[0];
            audioSource.Play();
            Debug.Log("Select");
            active = false;
            Invoke(nameof(Active), 0.1f);
        }
        else if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0) || sliderCount[0] != sliders[0].value || sliderCount[1] != sliders[1].value) && active)
        {
            audioSource.clip = sfx[1];
            if(pause == null) audioSource.Play();
            else if(pause.activeInHierarchy) audioSource.Play();
            sliderCount[0] = sliders[0].value;
            sliderCount[1] = sliders[1].value;
            PlayerPrefs.SetFloat("sfx", sliderCount[0]);
            PlayerPrefs.SetFloat("ost", sliderCount[1]);
            if (sliderCount[0] > 0.02f) audioMixer.audioMixer.SetFloat("sfx", -20 + 20 * sliderCount[0]);
            else audioMixer.audioMixer.SetFloat("sfx", -80);
            if (sliderCount[1] > 0.02f) audioMixer.audioMixer.SetFloat("ost", -20 + 20 * sliderCount[1]);
            else audioMixer.audioMixer.SetFloat("ost", -80);
            Debug.Log("Click");
            active = false;
            Invoke(nameof(Active), 0.1f);
        }
        button = EventSystem.current.currentSelectedGameObject;
    }
    public void Quit()
    {
        Application.Quit();
    }
    void Active()
    {
        active = true;
    }
}
