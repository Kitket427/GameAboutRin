using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private float rotate;
    [SerializeField] private float speed;
    private Transform target;
    private float rotateZ;
    [SerializeField] private float extraRotate;
    [SerializeField] private float randomExtraRotate;
    [SerializeField] private float speedExtraRotate;
    [SerializeField] private float timeExtraRotate;
    private float time;
    private void Start()
    {
        rotate = transform.eulerAngles.z;
        target = FindObjectOfType<AimPosPlayer>().GetComponent<Transform>();
        extraRotate += Random.Range(0f, randomExtraRotate);
    }
    void Update()
    {
        if (extraRotate > 0)
        {
            time += Time.deltaTime;
            if (time >= extraRotate && time < extraRotate + timeExtraRotate && speed < speedExtraRotate)
            {
                speed += speedExtraRotate;
            }
            else if (speed > speedExtraRotate) speed -= speedExtraRotate;
        }
        Vector3 difference = target.position - transform.position;
        rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (rotateZ > rotate) rotate += Time.deltaTime * speed;
        if (rotate > 0 && rotate - rotateZ > 180) rotate -= 360;
        if (rotateZ < rotate) rotate -= Time.deltaTime * speed;
        if (rotate < 0 && rotateZ - rotate > 180) rotate += 360;
        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }
}