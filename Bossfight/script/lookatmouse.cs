using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatmouse : MonoBehaviour
{
    public bool right = false;

    void Update()
    {
        look();
    }

    public void look()
    {
        Vector3 mouseposition = Input.mousePosition;
        mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);

        if (transform.position.x - mouseposition.x >= 0 && transform.localScale.x >= 0)
        {
            right = true;
            Vector3 vc = transform.localScale;
            vc.x *= -1;
            transform.localScale = vc;
        }
        if (transform.position.x - mouseposition.x < 0 && transform.localScale.x < 0)
        {
            right = false;
            Vector3 vc = transform.localScale;
            vc.x *= -1;
            transform.localScale = vc;
        }
    }
}
