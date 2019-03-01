using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ゲームオーバー、ゲームクリアの処理
public class GameEndControll : MonoBehaviour
{
    public GameObject button;
    public Text goal;
    public Text Battery;

    private AudioSource menu_se;
    private string scene_name;

    //UIの初期化
    public void UIInitialize()
    {
        button.SetActive(false);
        goal.enabled = false;
        Battery.enabled = false;
        menu_se = this.gameObject.GetComponent<AudioSource>();
    }

    //ゴール処理
  public void GoalUI()
    {
        button.SetActive(true);
        goal.enabled = true;
    }

    //ゲームオーバー処理
   public void GameOverUI()
    {
        button.SetActive(true);
        Battery.enabled = true;
    }

    //「もう一度遊ぶ」ボタンの処理
    public void onemore()
    {
        StartCoroutine(menu("Stage"));
    }

    //「タイトルへ戻る」ボタンの処理
    public void returntitle()
    {
        StartCoroutine(menu("Title"));
    }

    //効果音を鳴らすために遅らせてシーン移動
    private IEnumerator menu(string scene)
    {
        menu_se.PlayOneShot(menu_se.clip);
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(scene)   ;
    }
}
