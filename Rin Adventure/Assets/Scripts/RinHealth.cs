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
            if (damage < 99) lifes -= 1;
            else lifes = 0;
            UIactive();
            InvokeRepeating(nameof(Blink), 0, 0.07f);
            Invoke(nameof(BlinkStop), 1.2f);
            Instantiate(effect, transform.position, Quaternion.identity);
            shield = true;
            Debug.Log("Hit");
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
        }
        else
        {
            foreach (var sprite in sprites)
            {
                sprite.material = materials[1];
            }
            blink = true;
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
    }
}
