using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
enum Type
{
    collider, start
}
public class TriggerLevel : MonoBehaviour
{
    [SerializeField] private Trigger[] triggers;
    [SerializeField] private Type type;
    [SerializeField] private float time;
    private void Start()
    {
        if(type == Type.start)
        {
            Invoke(nameof(TriggerActivate), time);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rindik player = collision.GetComponent<Rindik>();
        if(player != null)
        {
            Invoke(nameof(TriggerActivate), time);
        }
    }
    void TriggerActivate()
    {
        foreach (var trigger in triggers)
        {
            trigger.obj.SetActive(trigger.active);
        }
        gameObject.SetActive(false);
    }
}
