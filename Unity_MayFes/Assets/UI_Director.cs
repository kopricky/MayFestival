using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Director : MonoBehaviour {

    Vector2[] speed;
    //バーの高さの最大値
    float height;
    int gang_count;
    List<GameObject> bar;
    List<GameObject> gang;
    Vector2 left;
    Vector2[] bar_pos = new Vector2[30];
    Vector2[] bar_size = new Vector2[30];
    int[] range_count = new int[30];
    //バーの速度に関する刻み幅
    float range = 0.01f;

	// Use this for initialization
	void Start () 
    {
        this.speed = GameObject.Find("Director").GetComponent<Director>().speed;
        this.height = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().height;
        bar = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().list_bar;
        gang = GameObject.Find("GangGenerator").GetComponent<Gang_Generator>().list_gang;
        for (int i = 0; i < bar.Count; i++)
        {
            bar_pos[i] = bar[i].transform.position;
        }
        for (int i = 0; i < bar.Count; i++)
        {
            bar_size[i] = bar[i].transform.localScale;
        }
            this.left = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().left;
	}
	
	// Update is called once per frame
	void Update () 
    {
        gang_count = 0;
        for (int i = 0; i < bar.Count; i++)
        {
            range_count[i] = 0;
        }
        //速度の各刻み範囲に含まれる暴走族の数
        for (int i = 0; i < bar.Count; i++)
        {
            int buff = (int)(this.speed[i].magnitude / range);
            if (buff >= bar.Count)
            {
                buff = bar.Count - 1;
            }
            range_count[buff]++;
        }
        //バーを更新
        for (int i = 0; i < bar.Count; i++)
        {
            //速度の各刻み範囲に含まれる暴走族の割合
            float rate = range_count[i] / (float)gang.Count;
            bar_pos[i].y = left.y + rate * height / 2.0f;
            bar[i].transform.position = bar_pos[i];
            bar_size[i].y = rate * height;
            bar[i].transform.localScale = bar_size[i];
        }
	}
}
