using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲーム本編の状態を管理するスクリプト
public class SceneControll : MonoBehaviour
{

    public enum GameState
    {
        Start = 1,
        Main,
        GameOver,
        Goal
    }
    GameState gamestate;

    public GameObject Player;
    public GameObject Goal;
    public GameObject countobj;
    public GameObject gamendobj;
    public Text count;

    private AudioSource gameover_se;
    private RobotControll robot;        //プレイヤーのスクリプト
    private GoalControll goal;         
    private GameEndControll end;        //「ゴール」「電池切れ」のUI表示するためのもの
    private float start_time = 4.0f;    //カウントダウン変数
    private bool mainflag = false;
    private bool gameoverflag = false;  //ゲームオーバーのSEフラグ

    // Start is called before the first frame update
    void Start()
    {
        //GetComponentの処理
        Player = GameObject.FindGameObjectWithTag("Player");
        Goal = GameObject.Find("Goal");
        count = countobj.GetComponent<Text>();
        robot = Player.GetComponent<RobotControll>();
        goal = Goal.GetComponent<GoalControll>();
        end = gamendobj.GetComponent<GameEndControll>();

        end.UIInitialize();

        //ゲームオーバーSEの取得
        AudioSource[] audio = GetComponents<AudioSource>();
        gameover_se = audio[1];

        //スタート時に効果音を鳴らす
        AudioSource start_se = audio[2];
        gamestate = GameState.Start;
        start_se.PlayOneShot(start_se.clip);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gamestate)
        {
            //カウントダウンの処理
            case GameState.Start:
                start_time -= Time.deltaTime;
                if (start_time > 1)
                {  
                    int time = (int)start_time;
                    count.text = time.ToString();
                }
                if(start_time<=1)
                {
                    count.text = "Start!";
                }
                if (start_time < 0)
                {
                    count.enabled = false;
                    robot.animstart();
                    gamestate = GameState.Main;
                }
                break;
            //ゲーム本編
            case GameState.Main:
                MainInitialize();
                if(robot.gameoverflag == true)
                {
                    gamestate = GameState.GameOver;
                }
                if(goal.Goalflag == true)
                {
                    gamestate = GameState.Goal;
                }
                break;
            //ゲームオーバー時の処理
            case GameState.GameOver:
                if(gameoverflag == false)
                {
                    gameover_se.PlayOneShot(gameover_se.clip);
                    gameoverflag = true;
                }
                end.GameOverUI();
                break;

            //ゴール時の処理
            case GameState.Goal:
                robot.playerstate = RobotControll.PlayerState.Goal;
                end.GoalUI();
                break;
        }
    }

    //カウントダウンが終わったら処理を開始する
    void MainInitialize()
    {
        if(mainflag == false)
        {
            robot.movespeed = 30.0f;
            robot.playerstate = RobotControll.PlayerState.Dash;
            mainflag = true;
        }
    }
}
