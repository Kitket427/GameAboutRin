using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RinHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int lifes, maxLifes;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Animator anim;
    public bool shield;
    [SerializeField]private SpriteRenderer[] sprites;
    [SerializeField] private Material[] materials;
    [SerializeField] private bool blink;
    [SerializeField] private GameObject effect;
    private void Start()
    {
        UIactive();
        foreach (var sprite in sprites)
        {
            sprite.material = materials[0];
        }
    }
    private void UIactive()
    {
        hearts[0].fillAmount = lifes * 1f / 10f;
        hearts[1].fillAmount = maxLifes * 1f / 10f;
        anim.speed = maxLifes * 1f / lifes * 1f;
    }
    void ITakeDamage.TakeDamage(int damage)
    {
        if (shield == false)
        {
            lifes -= damage;
            UIactive();
            InvokeRepeating(nameof(Blink), 0, 0.07f);
            Invoke(nameof(BlinkStop), 2.5f);
            Instantiate(effect, transform.position, Quaternion.identity);
            shield = true;
        }
    }
    void Blink()
    {
        if(blink)
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[0];
            }
            blink = false;
            Debug.Log("Material 1");
        }
        else
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[1];
            }
            blink = true;
            Debug.Log("Material 0");
        }
    }
    void BlinkStop()
    {
        CancelInvoke();
        shield = false;
        foreach (var sprite in sprites)
        {
            sprite.material = materials[0];
        }
        Debug.Log("Material Stop");
    }
}
