using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float playerSpeed;
    public bool jump = true;
    public float timer;
    public float timer2;
    public float timer3;
    public float timer4;
    public bool can_bang = true;
    public Animator animator;
    public Rigidbody2D rigi;
    public bool canJump = true;
    public bool canDoubleJump = true;
    public int jump_count = 1;
    public GameObject game;
    public int Cri_Chance;
    public int Cri_count = 0;
    public float My_DMG;
    public float DMG_show;
    public float playerHP;
    float bang = 0.25f;
    public float Supertime;
    public bool CooldownSupertime = false;

    float bossDmg = 200;

    float timer5;
    public float Echo_frequency;

    public GameObject Echo;

    //float fixedDeltaTime;//สร้างไว้เก็บค่าฟิคเดลต้าไทม์พื้นฐาน

    public Transform bullettranform;

    /*private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }*/

    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        playerHP = 50000;
    }

    private void FixedUpdate()
    {

    }

    void Update()
    {
        Debug.Log("dfsf");
        //DMG
        My_DMG = Random.Range(1000, 1500);
        Cri_Chance = Random.Range(0, 100);
        if (Cri_Chance <= 15 && CooldownSupertime == false)
        {
            My_DMG *= 50;
        }
        else if (Cri_Chance <= 85 && CooldownSupertime == true)
        {
            My_DMG *= 10;
        }

        if (playerHP > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            basic_Controller();

            fire();

            Jump();

            Supertime = Time.timeScale;

            if (Cri_count == 10 && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Ultimate is Working!!");
                Ultimate_SLOWMOTION();
            }

            if (Supertime != 1)
            {
                if (timer3 < 5f * Supertime * 2)
                {
                    timer3 += Time.deltaTime;
                }
                else
                {
                    Time.timeScale = 1;
                    timer3 = 0;
                }
                bang = 0.0125f;
                if (timer5 <= 0)
                {
                    GameObject c = (GameObject)Instantiate(Echo, transform.position, Quaternion.identity);
                    Destroy(c, 1f);
                    timer5 = Echo_frequency;
                }
                else
                {
                    timer5 -= Time.deltaTime;
                }
                CooldownSupertime = true;
            }
            else
            {
                if (timer4 < 3 && CooldownSupertime == true)
                {
                    timer4 += Time.deltaTime;
                }
                else
                {
                    CooldownSupertime = false;
                    timer4 = 0;
                }
                bang = 0.25f;
            }
        }
    }

    void Ultimate_SLOWMOTION()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;
        Cri_count = 0;
    }

    void basic_Controller()
    {
        float HorizontalMove = Input.GetAxis("Horizontal") * playerSpeed;

        HorizontalMove *= Time.deltaTime;

        transform.Translate(0, 0, HorizontalMove);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("walk", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("walk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
            animator.SetBool("walk", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump == true && jump_count == 1)
        {
            rigi.velocity = transform.up * 20;
            canJump = false;
            canDoubleJump = true;
            animator.SetBool("jump", true);
            jump_count = 2;
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump == true && jump_count == 2)
        {
            rigi.velocity = transform.up * 20;//rigi.addforce หลังใช้ไทม์สเกลแล้วมันโดดไม่ได้อีกเลย
            canJump = false;
            canDoubleJump = false;
            animator.SetBool("jump", true);
            jump_count = 0;
        }

        if (animator.GetBool("jump") == true && animator.GetBool("jump_down") == false)
        {
            if (timer2 < 0.25f)
            {
                timer2 += Time.deltaTime;
            }
            else
            {
                timer2 = 0;
                animator.SetBool("jump", false);
                animator.SetBool("jump_down", true);
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            timer2 = 0;
            animator.SetBool("jump", false);
            animator.SetBool("jump_down", true);
        }
    }

    void fire()
    {
        if (timer < bang && can_bang == false)
        {
            //animator.SetBool("bang", false);
            timer += Time.deltaTime;
        }
        else
        {
            can_bang = true;
            timer = 0;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetMouseButton(0) || !Input.GetKey(KeyCode.D) && !Input.GetMouseButton(0) || Input.GetMouseButton(0)/*|| !Input.GetKey(KeyCode.A) && Input.GetMouseButton(0) || !Input.GetKey(KeyCode.D) && Input.GetMouseButton(0)*/)
        {
            animator.SetBool("!_walk_bang", true);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetMouseButton(0) || Input.GetKey(KeyCode.D) && Input.GetMouseButton(0))
        {
            animator.SetBool("!_walk_bang", false);
        }

        if (Input.GetMouseButton(0) && can_bang == true)
        {
            animator.SetBool("bang_down", false);
            animator.SetBool("bang", true);
            can_bang = false;
            timer = 0;
            Instantiate(game, bullettranform.position, bullettranform.rotation);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("bang", false);
            animator.SetBool("bang_down", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground_base"))
        {
            jump_count = 1;
            canJump = true;
            canDoubleJump = true;
            animator.SetBool("jump_down", false);

            if (animator.GetBool("jump") == true)
            {
                animator.SetBool("jump", false);
                animator.SetBool("jump_down", false);
            }
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        canJump = false;
    }*/

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log(playerHP);
            playerHP--;
        }
    }*/

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            bossDmg += 10;
            if (bossDmg == 1000)
            {
                bossDmg = 1000;
            }
            playerHP -= bossDmg;
        }
    }
}
