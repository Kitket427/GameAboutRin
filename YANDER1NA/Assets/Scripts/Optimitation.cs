using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;
using UnityEngine.UI;
public class Optimitation : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private Slider slider;
    [SerializeField] private UniversalRenderPipelineAsset asset;
    [SerializeField] private ParticleSystem particle;
    
    private void Awake()
    {
        if (volume)
        {
            volume = FindObjectOfType<Volume>();
            PlayerPrefs.SetInt("PP", 1);
            volume.enabled = true;
        }
        if (slider)
        {
            if(PlayerPrefs.HasKey("render"))slider.value = PlayerPrefs.GetFloat("render");
        }
        if(particle)
        {
            particle.emissionRate = 777 * asset.renderScale * asset.renderScale;
        }
    }
    private void Update()
    {
        if (slider)
        {
            asset.renderScale = slider.value;
            GraphicsSettings.renderPipelineAsset = asset;
            PlayerPrefs.SetFloat("render", slider.value);
        }
    }
}
