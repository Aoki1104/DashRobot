using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのスクリプト
public class RobotControll : MonoBehaviour
{
    public enum PlayerState
    {
        Start = 1,
        Dash,
        Damage,
        GameOver,
        Goal
    };
    public PlayerState playerstate;

    public int maxbattery;
    public float movespeed;
    public GameObject battery_obj;
    public BatteryUI battery;
    public bool gameoverflag;

    private GameObject player;
    private Rigidbody player_rigid; 
    private Animator robotanim;
    private Vector3 Initialize_position;
    private Vector3 now_position;
    private Vector3 move_position;
    private bool right_move;
    private bool left_move;
    private float nowbattery;
    private float damage_time;

    [SerializeField]private float side_move;
    [SerializeField]private float moveForceMultiplier;
    

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 0;
        side_move = 0;
        robotanim = this.gameObject.GetComponent<Animator>();
        player = this.GetComponent<GameObject>();
        player_rigid = this.GetComponent<Rigidbody>();
        Initialize_position = this.transform.position;

        battery = battery_obj.GetComponent<BatteryUI>();
        nowbattery = maxbattery;
        battery.batteryInitialize(maxbattery);

        playerstate = PlayerState.Start;
    }

    public void animstart()
    {
        robotanim.SetBool("Dash", true);
    }

    // Update is called once per frame
    void Update()
    {
        now_position = this.transform.position;

        switch (playerstate)
        {
            case PlayerState.Start:
                //カウントダウン中は何もしない
                break;

            //基本動作
            case PlayerState.Dash:
                sidemove();
                BatteryUpdate();
                break;

            case PlayerState.Damage:
                sidemove();
                
                //無敵時間の処理
                damage_time -= Time.deltaTime;
                if (damage_time < 0)
                {
                    playerstate = PlayerState.Dash;
                }

                BatteryUpdate();
                break;

            case PlayerState.GameOver:
                movespeed = 0;
                gameoverflag = true;
                break;

            case PlayerState.Goal:
                movespeed = 0;
                break;
        }

    }

    //横の移動処理
    private void sidemove()
    {
        //左右の移動処理関連
        if (right_move != true)
        {
            if (now_position.x >= Initialize_position.x && left_move == false)
                if (Input.GetKeyDown(KeyCode.A))
                {
                    side_move = -5;
                    move_position = this.transform.position;
                    left_move = true;

                }
        }

        if (left_move != true)
        {
            if (now_position.x <= Initialize_position.x && right_move == false)
                if (Input.GetKeyDown(KeyCode.D))
                {
                    side_move = 5;
                    move_position = this.transform.position;
                    right_move = true;
                }
        }

        //左の移動
        if (left_move == true)
        {
            if (now_position.x <= move_position.x - 9)
            {
                side_move = 0;
                //位置調整
                if (this.transform.position.x != move_position.x - 9)
                {
                    this.transform.position = new Vector3(move_position.x - 9,
                                                          this.transform.position.y,
                                                          this.transform.position.z);
                }
                left_move = false;
            }
        }

        //右の移動
        if (right_move == true)
        {
            if (now_position.x >= move_position.x + 9)
            {
                side_move = 0;
                //位置調整
                if (this.transform.position.x != move_position.x + 9)
                {
                    this.transform.position = new Vector3(move_position.x + 9,
                                                          this.transform.position.y,
                                                          this.transform.position.z);
                }
                right_move = false;
            }
        }
    }

    //バッテリーUIの更新
    private void BatteryUpdate()
    {
        //バッテリーUIの更新
        nowbattery -= Time.deltaTime;
        battery.battery_update(nowbattery);

        //バッテリーが0になったらゲームオーバーになる
        if (nowbattery < 0)
        {
            playerstate = PlayerState.GameOver;
        }
    }

    private void FixedUpdate()
    {
       //移動の処理
            Vector3 moveVector = Vector3.zero;
        if (playerstate == PlayerState.Dash || playerstate == PlayerState.Damage)
        {
            moveVector.x = movespeed * side_move;
            moveVector.z = movespeed;
        }
        if(playerstate == PlayerState.GameOver)
        {
            moveVector.x = 0;
            moveVector.z = 0;
        }
        player_rigid.AddForce(moveForceMultiplier * (moveVector - player_rigid.velocity));
        
        }

    private void OnTriggerEnter(Collider other)
    {
        //各オブジェクトとぶつかったときの処理
        //障害物はダメージを受けてる場合はぶつかっても何もオキない
        if (playerstate != PlayerState.Damage)
        {
            if (other.gameObject.tag == "Enemy")
            {
                nowbattery -= 2;
                damage_time = 0.5f;
                playerstate = PlayerState.Damage;
            }
            if (other.gameObject.tag == "Battery")
            {
                nowbattery += 3;
            }
        }

        if(other.gameObject.tag == "Goal")
        {
            playerstate = PlayerState.Goal;
        }
    }
}
