using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
enum BonusType
{
    power, heart, heartFull
}
public class Bonus : MonoBehaviour
{
    [SerializeField] private BonusType bonusType;
    [SerializeField] private int seconds;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rindik rindik = collision.GetComponent<Rindik>();
        if(rindik != null && bonusType == BonusType.power)
        {
            rindik.Power(seconds);
            Destroy(gameObject);
        }
        RinHealth rindikH = collision.GetComponent<RinHealth>();
        if(rindikH != null && bonusType != BonusType.power)
        {
            if (bonusType == BonusType.heart) rindikH.Heal(1);
            if (bonusType == BonusType.heartFull) rindikH.Heal(4);
            Destroy(gameObject);
        }
    }
}
