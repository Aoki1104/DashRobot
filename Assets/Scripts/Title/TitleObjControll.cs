using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトルのカメラの処理
public class TitleObjControll : MonoBehaviour
{
    public GameObject point;

    private Vector3 point_position;
    private void Start()
    {
        point = GameObject.Find("Point");
        point_position = point.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(point_position, Vector3.up, Time.deltaTime * 3);
    }
}
