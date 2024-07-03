using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    AsyncOperation asyncOperation;
    [SerializeField] private Image LoadBar;
    [SerializeField] private Text BarTxt;
    [SerializeField] private int SceneID;
    [SerializeField] private bool newGame;
    private void Start()
    {
        if(SceneID == 427)
        {
            SceneID = 0;
        }
        else if(newGame || PlayerPrefs.HasKey("SaveLevel") == false)
        {
            SceneID = 1;
        }
        else
        {
            SceneID = PlayerPrefs.GetInt("SaveLevel");
        }
        Invoke(nameof(StartLoad), 1);
    }
    void StartLoad()
    {
        StartCoroutine(LoadSceneCor());
    }

    IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(SceneID);
        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            LoadBar.fillAmount = progress;
            //BarTxt.text = "Загрузка " + string.Format("{0:0}%", progress * 100f);
            yield return 0;
        }
    }

}
