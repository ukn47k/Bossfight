using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatus : MonoBehaviour
{
    public float Boss_HP;
    int a;
    int b;
    float timer;
    bool right = false;
    public GameObject FloatingText;
    public GameObject Float_transfrom;
    public Transform playerTransform;
    public GameObject stone;

    public Text text;
    public Text text2;

    playerController player;

    public bool can_jump = true;

    Rigidbody2D rigi;

    private void Start()
    {
        player = GameObject.Find("player").GetComponent<playerController>();
        rigi = GetComponent<Rigidbody2D>();

        text = GameObject.Find("Cri_Count_Text").GetComponent<Text>();
        text2 = GameObject.Find("GameOver").GetComponent<Text>();

        Boss_HP = 1000000;
    }

    private void Update()
    {
        if (player.playerHP <= 0)
        {
            text2.text = "Press R For Restart";
        }
        if (Boss_HP <= 0)
        {
            text2.text = "You Win";
        }

        flip();
        if (Boss_HP > 0)
        {
            if (timer < Boss_HP / 1000000 /*/player.Supertime*/)
            {
                timer += Time.deltaTime;
            }
            else
            {
                b = Random.Range(0, 3);
                if (b == 0) { bossJump(); }
                else if (b == 1) { bossRun(); }
                else { bossStand(); }
                timer = 0;
            }
        }
    }

    void bossJump()
    {
        if (can_jump == true)
        {
            rigi.velocity = transform.up * 20;
            can_jump = false;
        }
    }

    void bossRun()
    {
        a = Random.Range(0, 2);
        if (a == 1)
        {
            rigi.velocity = transform.right * -50 * player.Supertime;
        }
        else
        {
            rigi.velocity = transform.right * 50 * player.Supertime;
        }
    }

    void bossStand()
    {
        rigi.AddForce(Vector2.zero);
    }

    public void Boss_takedmg(float dmg)
    {
        if (player.Cri_Chance <= 15)
        {
            player.Cri_count++;
        }
        if (player.Cri_count >= 10) {
            player.Cri_count = 10;
        }
        
        text.text = player.Cri_count.ToString();
        Boss_HP -= dmg;
    }

    public void flip()
    {
        if (playerTransform.position.x >= transform.position.x && transform.localScale.x >= 0)
        {
            right = true;
            Vector3 vc = transform.localScale;
            vc.x *= -1;
            transform.localScale = vc;
        }
        if (playerTransform.position.x < transform.position.x && transform.localScale.x < 0)
        {
            right = false;
            Vector3 vc = transform.localScale;
            vc.x *= -1;
            transform.localScale = vc;
        }
    }

    public void ShowFloatText()
    {
        Instantiate(FloatingText, Float_transfrom.transform.position,Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground_base"))
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(stone, new Vector3(Random.Range(-25f, 25f), Random.Range(20f, 30f), 0), Quaternion.identity);
            }
            can_jump = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            can_jump = true;
        }
    }
}
