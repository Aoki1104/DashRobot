using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ボタン＆シーン移動関連の処理
public class TitleControll : MonoBehaviour
{

    private AudioSource menu_se;

    private void Start()
    {
        menu_se = this.gameObject.GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        StartCoroutine(menu("Start"));
    }
    public void End()
    {
        StartCoroutine(menu("End"));
    }

    private IEnumerator menu(string choice)
    {
        menu_se.PlayOneShot(menu_se.clip);
        yield return new WaitForSeconds(0.8f);
        if (choice == "Start")
            SceneManager.LoadScene("Stage");
        if(choice == "End")
            Application.Quit();
    }
}
