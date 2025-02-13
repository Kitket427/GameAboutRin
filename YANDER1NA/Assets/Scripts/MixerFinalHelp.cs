using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerFinalHelp : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixerGroup;
    // Start is called before the first frame update
    void Start()
    {
        mixerGroup.audioMixer.SetFloat("gameSpeed", 1);
        mixerGroup.audioMixer.SetFloat("ostSpeed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
