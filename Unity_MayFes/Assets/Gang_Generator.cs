using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gang_Generator : MonoBehaviour {

    public GameObject GangPrefab;
    public List<GameObject> list_gang = new List<GameObject>();
    //初期位置を表す行と列
    int row=5;
    int column=5;
    //暴走族と警察の動く範囲(高さと幅)
    float height=10.0f;
    float wide=12.0f;
    //画面の左下の座標
    Vector2 leftdown = new Vector2(-8.0f,-5.0f);

	// Use this for initialization
	void Awake () 
    {
        for (int i = 1; i <= row; i++)
        {
            for (int j = 1; j <= column; j++)
            {
                //プレハブを生成してリストで保持する。
                GameObject go = Instantiate(GangPrefab) as GameObject;
                //暴走族の初期位置の座標を設定
                go.transform.position = leftdown + new Vector2(j*wide/(row+1), i*height/(column+1));
                list_gang.Add(go);
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
