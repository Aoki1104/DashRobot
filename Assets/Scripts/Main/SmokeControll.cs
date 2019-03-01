using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの視界を狭くするためのエフェクト
public class SmokeControll : MonoBehaviour
{
    public GameObject player;

    private Vector3 smokepositon;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        smokepositon = new Vector3(0.0f,
                                   0.0f,
                                   player.transform.position.z + 75);
    }
    
    void Update()
    {
        //プレイヤーの位置と同期して移動する
        smokepositon = new Vector3(0.0f,
                           0.0f,
                           player.transform.position.z + 75);
        this.gameObject.transform.position = smokepositon;
    }
}
