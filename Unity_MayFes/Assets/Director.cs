//大幅に修正の必要ありです

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour {

    List<GameObject> gang;
    float[,] dir = new float[100,100];
    public Vector2[] speed = new Vector2[10001];

	// Use this for initialization
	void Start () 
    {
        gang = GameObject.Find("GangGenerator").GetComponent<Gang_Generator>().list_gang;
        for (int i = 0; i < 10000; i++)
        {
            speed[i].x = 0.1f;
            speed[i].y = 0.1f;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        //全点対間の距離を求める。
        for (int i = 0; i < gang.Count; i++)
        {
            for (int j = 0; j < gang.Count; j++)
            {
                dir[i,j] = (gang[i].transform.position - gang[j].transform.position).magnitude;
            }
        }
        //速度に応じて色を変更する。
        for (int i = 0; i < gang.Count; i++)
        {
            if (speed[i].magnitude > 0.15f)
            {
                gang[i].GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            if (speed[i].magnitude < 0.05f)
            {
                gang[i].GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        //test
        /*
        for (int i = 0; i < gang.Count; i++)
        {
            for (int j = 0; j < gang.Count; j++)
            {
                if (gang[j].GetComponent<SpriteRenderer>().color == Color.magenta)
                {
                    if (dir[i, j] < 0.4f)
                    {
                        if (i != j && speed[i].magnitude < 0.24f)
                        {
                            speed[i] *= 1.2f;
                        }
                    }
                }
            }
            for (int j = 0; j < gang.Count; j++)
            {
                if (gang[j].GetComponent<SpriteRenderer>().color == Color.cyan)
                {
                    if (dir[i, j] < 0.4f)
                    {
                        if (i != j && speed[i].magnitude > 0.01f)
                        {
                            speed[i] *= 5.0f / 6.0f;
                        }
                    }
                }
            }
        }
         * */
        //各点の移動
        for (int i = 0; i < gang.Count; i++)
        {
            gang[i].transform.Translate(speed[i].x, speed[i].y, 0);
        }
        //境界処理
        for (int i = 0; i < gang.Count; i++)
        {
            if (gang[i].transform.position.x < -7.9f || gang[i].transform.position.x > 3.9f)
            {
                speed[i].x *= -1;
            }
            if (gang[i].transform.position.y < -4.9f || gang[i].transform.position.y > 4.9f)
            {
                speed[i].y *= -1;
            }
        }
    }
}
