using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private float rotate;
    [SerializeField] private float speed;
    private Transform target;
    private float rotateZ;
    private void Start()
    {
        rotate = transform.eulerAngles.z;
        target = FindObjectOfType<Rindik>().GetComponent<Transform>();
    }
    void Update()
    {
        Vector3 difference = target.position - transform.position;
        rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (rotateZ > rotate) rotate += Time.deltaTime * speed;
        if (rotate > 0 && rotate - rotateZ > 180) rotate -= 360;
        if (rotateZ < rotate) rotate -= Time.deltaTime * speed;
        if (rotate < 0 && rotateZ - rotate > 180) rotate += 360;
        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }
}