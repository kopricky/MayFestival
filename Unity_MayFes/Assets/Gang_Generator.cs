using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gang_Generator : MonoBehaviour {

    public GameObject GangPrefab;
    public List<GameObject> list_gang = new List<GameObject>();
    int row=5;
    int column=5;
    float height=10.0f;
    float wide=12.0f;
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
