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
    [SerializeField] private bool survial;
    private void Start()
    {
        if (survial == false)
        {
            if (SceneID != -1)
            {
                if (SceneID != 0)
                {
                    if (PlayerPrefs.GetInt("GameOver") > 15) PlayerPrefs.SetInt("GameOver", 15);
                    PlayerPrefs.SetInt("GameOver", (PlayerPrefs.GetInt("GameOver") - 1));
                    if (PlayerPrefs.GetInt("GameOver") < 0) PlayerPrefs.SetInt("GameOver", 0);
                    while (PlayerPrefs.GetInt("GameOver") % 3 != 0) PlayerPrefs.SetInt("GameOver", (PlayerPrefs.GetInt("GameOver") - 1));
                }
            }
            else if (newGame || PlayerPrefs.HasKey("SaveLevel") == false)
            {
                PlayerPrefs.DeleteKey("SaveLevel");
                PlayerPrefs.DeleteKey("GameOver");
                PlayerPrefs.DeleteKey("Death");
                PlayerPrefs.DeleteKey("Time");
                SceneID = 1;
            }
            else
            {
                SceneID = PlayerPrefs.GetInt("SaveLevel");
            }
        }
        else SceneID = 27;
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
