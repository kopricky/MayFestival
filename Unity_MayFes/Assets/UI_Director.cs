using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Director : MonoBehaviour {

    Vector2[] speed;
    float height;
    int gang_count;
    List<GameObject> bar;
    Vector2 left;
    float[] cri = new float[30];
    int[] range_count = new int[30];
    float range = 0.01f;
    public int bar_count = 29;

	// Use this for initialization
	void Start () 
    {
        this.speed = GameObject.Find("Director").GetComponent<Director>().speed;
        this.height = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().height;
        bar = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().list_bar;
        this.left = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().left;
        for (int i = 0; i < bar_count; i++)
        {
            cri[i] = range * i;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        gang_count = 0;
        for (int i = 0; i < bar_count; i++)
        {
            range_count[i] = 0;
        }
        for (int i = 0; i < bar_count; i++)
        {
            int buff = (int)(this.speed[i].magnitude / range);
            if (buff >= bar_count)
            {
                buff = bar_count - 1;
            }
            range_count[buff]++;
        }   
        for (int i = 0; i < bar_count; i++)
        {
            gang_count += range_count[i]++;
        }
        //バーを更新
        for (int i = 0; i < bar_count; i++)
        {
            float rate = range_count[i] / (float)gang_count;
            Vector3 temp = bar[i].transform.position;
            temp.y = left.y + rate * height / 2.0f;
            bar[i].transform.position = temp;
            temp = bar[i].transform.localScale;
            temp.y = rate*height;
            bar[i].transform.localScale = temp;
        }
	}
}
