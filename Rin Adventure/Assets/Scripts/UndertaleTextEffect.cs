using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
struct TextLine
{
    public string text;
    public float speed, timeAlpha, timeNext;
}
public class UndertaleTextEffect : MonoBehaviour
{
    [SerializeField] private Text textObject;
    [SerializeField] private float time;
    [SerializeField] private TextLine[] messages;
    private int currentMessageIndex = 0;
    private string currentText = "";
    private AudioSource voice;
    private void Start()
    {
        voice = GetComponent<AudioSource>();
        textObject.text = "";
        Invoke(nameof(StartText), -time);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (messages[currentMessageIndex].timeAlpha - time < 1) textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, messages[currentMessageIndex].timeAlpha - time);
        else textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, 1);
    }
    void StartText()
    {
        StartCoroutine(ShowText());
    }
    IEnumerator ShowText()
    {
        time = 0;
        Invoke(nameof(NextMessage), messages[currentMessageIndex].timeNext);
        string message = messages[currentMessageIndex].text;
        for (int i = 0; i <= message.Length; i++)
        {
            currentText = message.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(1/messages[currentMessageIndex].speed);
            voice.Play();
        }
    }
    private void NextMessage()
    {
        if (currentMessageIndex < messages.Length - 1)
        {
            currentMessageIndex++;
            textObject.text = "";
            StartCoroutine(ShowText());
        }
        else gameObject.SetActive(false);
    }
}