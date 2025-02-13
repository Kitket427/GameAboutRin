using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    [SerializeField] private int gameOvers;
    private void Awake()
    {
        PlayerPrefs.SetInt("GameOver", PlayerPrefs.GetInt("GameOver") + gameOvers);
    }
    void Start()
    {
        PlayerPrefs.SetInt("SaveLevel", SceneManager.GetActiveScene().buildIndex);
    }
}
