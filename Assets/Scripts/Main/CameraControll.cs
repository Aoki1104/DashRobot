using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラのスクリプト
//常にプレイヤーの後ろに配置する
public class CameraControll : MonoBehaviour
{
    public GameObject player;

    private Vector3 Player_position;
    private Quaternion Camera_angle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camera_angle = new Quaternion(20.0f, 0.0f, 0.0f,1.0f);
    }

    
    void Update()
    {

        Player_position = player.transform.position;
        this.transform.position = new Vector3(Player_position.x,
                                              Player_position.y + 10,
                                              Player_position.z -12.0f);
    }
}
