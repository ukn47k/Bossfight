using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMG_text : MonoBehaviour
{
    TextMesh textMesh;
    Color color;
    playerController playerController;
    Rigidbody2D rigi;
    public GameObject bg;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        textMesh = GetComponent<TextMesh>();
        playerController = GameObject.Find("player").GetComponent<playerController>();
        chance_color();

        int a = Random.Range(0, 3);
        if (a == 0)
        {
            rigi.velocity = transform.right * 10;
        }
        else if (a == 1)
        {
            rigi.velocity = transform.right * -10;
        }
        else if (a == 2)
        {
            rigi.velocity = transform.up * 10;
        }
        if (playerController.Cri_Chance <= 15 && playerController.CooldownSupertime == false)
        {
            rigi.velocity = transform.up * 10;
            gameObject.transform.localScale *= 2;
        }
        else if (playerController.Cri_Chance <= 85 && playerController.CooldownSupertime == true)
        {
            rigi.velocity = transform.up * 10;
            gameObject.transform.localScale *= 2;
        }
        Destroy(gameObject,3f);
    }

    public void chance_color()
    {
        if (playerController.Cri_Chance >= 15 && playerController.CooldownSupertime == false)
        {
            bg.SetActive(false);
            textMesh.text = (playerController.My_DMG.ToString());
            ColorUtility.TryParseHtmlString("#B3B3B3", out color);
            textMesh.color = color;
        }
        else if (playerController.Cri_Chance >= 85 && playerController.CooldownSupertime == true)
        {
            bg.SetActive(false);
            textMesh.text = (playerController.My_DMG.ToString());
            ColorUtility.TryParseHtmlString("#B3B3B3", out color);
            textMesh.color = color;
        }
        else
        {
            bg.SetActive(true);
            textMesh.text = (playerController.My_DMG.ToString());
            ColorUtility.TryParseHtmlString("#FF0000", out color);
            textMesh.color = color;
        }
    }
}
