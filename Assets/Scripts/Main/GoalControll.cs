using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゴールのスクリプト
//プレイヤーが当たったら処理を開始する
public class GoalControll : MonoBehaviour
{
    public bool Goalflag = false;

    private AudioSource goalse;

    private void Start()
    {
        goalse = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            goalse.PlayOneShot(goalse.clip);
            Goalflag = true;
        }
    }
}
