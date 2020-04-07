using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigi;

    public Transform transform_textPopup;

    lookatmouse lookatmouse;
    BossStatus bossStatus;
    playerController playerController;

    public GameObject partiple_bang_G;
    public GameObject partiple_bang_G2;

    void Start()
    {
        lookatmouse = GameObject.Find("player").GetComponent<lookatmouse>();
        bossStatus = GameObject.Find("Boss").GetComponent<BossStatus>();
        playerController = GameObject.Find("player").GetComponent<playerController>();

        if (lookatmouse.right == true)
        {
            rigi.velocity = transform.right * -speed;
        }
        else if (lookatmouse.right == false)
        {
            rigi.velocity = transform.right * speed;
        }
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (bossStatus.Boss_HP > 0)
            {
                bossStatus.Boss_takedmg(playerController.My_DMG);
                
                if (playerController.Cri_Chance <= 15) {
                    Destroy(Instantiate(partiple_bang_G, transform.position, Quaternion.identity), 5f);
                }
                else
                {
                    Destroy(Instantiate(partiple_bang_G2, transform.position, Quaternion.identity), 5f);
                }

                bossStatus.ShowFloatText();
                Destroy(gameObject);
            }
        }
    }

    /* กระสุนเวี่ยงตามกันหันของเม้า มันสามารถวิ่งไปกลับในหน้าจอได้
    private void Update()
    {
        if (lookatmouse.right == true)
        {
            rigi.velocity = transform.right * -speed;
        }
        else if (lookatmouse.right == false)
        {
            rigi.velocity = transform.right * speed;
        }
    }*/
}
