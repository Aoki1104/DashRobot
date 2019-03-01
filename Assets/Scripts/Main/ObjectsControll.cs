using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムや障害物の処理
//それぞれの効果はプレイヤーのスクリプトで行う
public class ObjectsControll : MonoBehaviour
{
    private AudioSource conact_se;

    private void Start()
    {
        conact_se = this.gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           StartCoroutine(destory());
        }
    }

    private IEnumerator destory()
    {
        
        conact_se.PlayOneShot(conact_se.clip);
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
