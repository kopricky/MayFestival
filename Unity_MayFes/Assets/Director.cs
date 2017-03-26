//大幅に修正の必要ありです
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour {

    List<GameObject> list_gang;
    float[,] dir = new float[100,100];
    Vector2 first_force;
    Rigidbody2D[] gang_rb = new Rigidbody2D[100];
	// Use this for initialization
	void Start () 
    {
        list_gang = GameObject.Find("GangGenerator").GetComponent<Gang_Generator>().list_gang;
        first_force.x = 100.0f;
        first_force.y = 100.0f;
        for (int i = 0; i < list_gang.Count; i++)
        {
            gang_rb[i] = list_gang[i].GetComponent<Rigidbody2D>();
            gang_rb[i].AddForce(first_force);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        //全点対間の距離を求める。
        for (int i = 0; i < list_gang.Count; i++)
        {
            for (int j = 0; j < list_gang.Count; j++)
            {
                dir[i,j] = (list_gang[i].transform.position - list_gang[j].transform.position).magnitude;
            }
        }
        //速度に応じて色を変更する。
        for (int i = 0; i < list_gang.Count; i++)
        {
            if (gang_rb[i].velocity.magnitude > 0.15f)
            {
                list_gang[i].GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            if (gang_rb[i].velocity.magnitude < 0.05f)
            {
                list_gang[i].GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        //境界処理
        for (int i = 0; i < list_gang.Count; i++)
        {
            if (list_gang[i].transform.position.x < -7.9f || list_gang[i].transform.position.x > 3.9f)
            {
                Vector2 force;
                force.x = 100.0f;
                force.y = 0.0f;
                gang_rb[i].AddForce(System.Math.Sign(list_gang[i].transform.position.x)*-force);
            }
            if (list_gang[i].transform.position.y < -4.9f || list_gang[i].transform.position.y > 4.9f)
            {
                Vector2 force;
                force.x = 0.0f;
                force.y = 100.0f;
                gang_rb[i].AddForce(System.Math.Sign(list_gang[i].transform.position.y)*-force);
            }
        }
    }
}