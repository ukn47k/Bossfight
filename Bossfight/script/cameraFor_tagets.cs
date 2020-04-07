using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class cameraFor_tagets : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;

    public float maxZoom;
    public float minZoom;
    public float zoomLimiter;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 centerPoint = GetCenterPoint();

        zoom();
        transform.position = centerPoint + offset;
    }

    void zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance()/zoomLimiter);
        cam.orthographicSize = newZoom;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) {
            return targets[0].position;//ถ้าเป้าหมายมีอันเดียวก็ช่างมัน กลับไปใช้แค่อันเดียว
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
