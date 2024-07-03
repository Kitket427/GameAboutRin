using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPosPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void Update()
    {
        if (player) transform.position = player.position;
    }
}
