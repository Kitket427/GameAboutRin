using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDead : MonoBehaviour
{
    [SerializeField]private Sprite[] sprites;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
