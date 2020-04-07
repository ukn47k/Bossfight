using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossUI : MonoBehaviour
{
    BossStatus BossStatus;

    private void Start()
    {
        BossStatus = GameObject.Find("Boss").GetComponent<BossStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossStatus.Boss_HP > 0)
        {
            transform.localScale = new Vector3(BossStatus.Boss_HP/ 1000000, 1, 1);
        }
        else
        {
            BossStatus.Boss_HP = 0;
            transform.localScale = new Vector3(0, 1, 1);
        }
    }
}
