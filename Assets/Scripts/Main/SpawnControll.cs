using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//障害物やアイテムを出現させる処理
public class SpawnControll : MonoBehaviour
{
    public GameObject[] objects;

    //出現する種類や位置が偏らないようにするための変数
    private int[] objects_weight = { 2,8 ,2}; //左から 無害な置物,障害物,バッテリー
    private int[] position_weight = { 10, 10, 10 }; //どこの位置に出ているかを確認する配列

    private float[] positon = { -9, 0, 9 }; //出現させるX軸の座標
    private int beforposi = 3;
    private Vector3 objposi;
    [SerializeField]private int spawn_max;

    private void Start()
    {
        //ゲーム開始時に生成
        for (int spawn_num = 0; spawn_num < spawn_max; spawn_num++)
        {
            spawn();
        }
    }

    //生成処理の関数
    void spawn()
    {
        int randcount = 0;
        for (int num = 1; num <= 10; num++)
        {
            int rand = Choose(objects_weight);

            if (rand == 0)
                randcount++;
            //最低でも1ブロックに１個はバッテリーを出す
            if (num== 10 && randcount == 0)
            {
                rand = 0;
            }

            //2つ以上のバッテリーは出ないようにする
            if(randcount >= 2)
            {
                rand = 1;
            }

            float posi = positon[Choose(position_weight)];
           
            objposi.x = posi;
            objposi.y = 3.5f;       //沈まないようにする
            objposi.z += 15.0f;     
            Quaternion rote = new Quaternion(0.0f, 90.0f, 0.0f, 1.0f);

            if (rand == 0)
            {
              rote = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            }
            Instantiate(objects[rand], objposi, rote);
            cut_weight(posi);//配置を偏らないようにする
            
        }
    }

    //抽選処理
    int Choose(int[] probs)
    {
        float total = 0;

        //トータルの重み付け
        foreach(float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;
        for(int i = 0; i < probs.Length; i++)
        {
            if(randomPoint < probs[i])
            {
                
                //抽選決定
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }
    
    //配置された位置ごとの重み付け
    void cut_weight(float posi)
    {
        int now_posi=0; //今回配置された位置
        switch (posi)
        {
            case -9:
                now_posi = 0;
                position_weight[0] = position_weight[0]/2;
                break;
            case 0:
                now_posi = 1;
                position_weight[1] = position_weight[1]/2;
                break;
            case 9:
                now_posi = 2;
                position_weight[2] = position_weight[2]/2;
                break;
        }

        //前回配置されていた位置はリセット
        if (beforposi < 3 && beforposi != now_posi)
        {
            position_weight[beforposi] = 10;
        }
        beforposi = now_posi;   //今回分の決定ポジションをセット
    }
}
