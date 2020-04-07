using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grass : MonoBehaviour
{
    public Animator animator;
    public GameObject particle_G;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("trigger", true);
            Instantiate(particle_G,transform.position,Quaternion.identity);
        }
        if (collision.gameObject.tag == "Boss")
        {
            animator.SetBool("trigger", true);
            Instantiate(particle_G, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("trigger", false);
        }
        if (collision.gameObject.tag == "Boss")
        {
            animator.SetBool("trigger", false);
        }
    }
}
