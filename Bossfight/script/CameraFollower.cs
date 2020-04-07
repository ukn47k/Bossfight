using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target1;
    public Transform target2;

    public Vector3 offset;

    float a;

    void Update()
    {
        Vector3 a = target1.position + target2.position;
        a = a / 2;
        Vector3 desired_position = a + offset;
        transform.position = desired_position;
    }
}
