using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//無害な置物にアタッチするスクリプト
//ワイヤーフレーム表示をする
public class WireFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Lines, 0);
    }
}
