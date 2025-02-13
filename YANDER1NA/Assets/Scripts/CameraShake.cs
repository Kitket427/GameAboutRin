using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeDuration = 0.5f;
    public float shakeAmplitude = 1.2f;

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    private System.Collections.IEnumerator Shake()
    {
        CinemachineBasicMultiChannelPerlin noise =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = shakeAmplitude;
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(shakeAmplitude, 0f, elapsed / shakeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        noise.m_AmplitudeGain = 0f;
    }
    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.G)) ShakeCamera();
    }
}