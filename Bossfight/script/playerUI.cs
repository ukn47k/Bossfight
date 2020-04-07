using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUI : MonoBehaviour
{
    playerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI_Method();
    }

    void playerUI_Method()
    {
        if (player.playerHP > 0)
        {
            transform.localScale = new Vector3(player.playerHP / 50000, 1, 1);
        }
        else
        {
            player.playerHP = 0;
            transform.localScale = new Vector3(0, 1, 1);
        }
    }
}
