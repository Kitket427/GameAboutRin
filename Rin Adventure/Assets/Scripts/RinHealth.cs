using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RinHealth : MonoBehaviour
{
    [SerializeField] private int lifes, maxLifes;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Animator anim;
    private void Start()
    {
        UIactive();
    }
    private void UIactive()
    {
        hearts[0].fillAmount = lifes * 1f / 10f;
        hearts[1].fillAmount = maxLifes * 1f / 10f;
        anim.speed = maxLifes * 1f / lifes * 1f;
    }
}
