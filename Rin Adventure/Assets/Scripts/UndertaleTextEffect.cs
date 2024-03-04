using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
struct TextLine
{
    public string text;
    public float time, speed;
}
public class UndertaleTextEffect : MonoBehaviour
{
    [SerializeField] private Text textObject;

    [SerializeField] private TextLine[] messages;
    private int currentMessageIndex = 0;
    private string currentText = "";

    private void Start()
    {
        textObject.text = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        Invoke(nameof(NextMessage), messages[currentMessageIndex].time);
        string message = messages[currentMessageIndex].text;
        for (int i = 0; i <= message.Length; i++)
        {
            currentText = message.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(1/messages[currentMessageIndex].speed);
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