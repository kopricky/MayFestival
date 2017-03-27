using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Director : MonoBehaviour
{

    List<GameObject> gang;
    float[,] dir = new float[100, 100];
    public Vector2[] speed;
    Vector2[] newSpeed = new Vector2[10001];
    Vector2 averageSpeed;
    Vector2 centerPosition;
    Vector2 avoidFrom;
    public float coAverage;
    public float r_ave;
    public float coCenter;
    public float r_cen;
    public float coAvoid;
    public float r_avo;
    public float normalSpeed;
    int count_ave;
    int count_cen;
    int count_avo;

    // Use this for initialization
    void Start()
    {
        gang = GameObject.Find("GangGenerator").GetComponent<Gang_Generator>().list_gang;
        speed = new Vector2[10001];
        for (int i = 0; i < 10000; i++)
        {
             speed[i].x = 0.1f;
             speed[i].y = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //全点対間の距離を求める。
        for (int i = 0; i < gang.Count; i++)
        {
            for (int j = 0; j < gang.Count; j++)
            {
                dir[i, j] = (gang[i].transform.position - gang[j].transform.position).magnitude;
            }
        }
        for (int i = 0; i < gang.Count; i++)
        {
            centerPosition = Vector2.zero;
            averageSpeed = Vector2.zero;
            avoidFrom = Vector2.zero;
            count_cen = 0;
            count_ave = 0;
            count_avo = 0;

            if (gang[i] == null)
            {
                continue;
            }
            for(int j = 0; j < gang.Count; j++)
            {
                if(gang[j] == null || i == j) { continue; }
                if (dir[i, j] < r_avo)
                {
                    avoidFrom += (Vector2)(gang[j].transform.position - gang[i].transform.position).normalized;
                    count_avo++;
                }
                else { 
                    if (dir[i, j] < r_ave)
                    {
                        averageSpeed += speed[j];
                        count_ave++;
                        if (dir[i, j] < r_cen)
                        {
                            centerPosition += (Vector2)gang[j].transform.position;
                            count_cen++;
                        }
                    }
                }
            }
            averageSpeed /= count_ave;
            centerPosition /= count_cen;

            newSpeed[i] = speed[i]+((coAverage * averageSpeed.normalized) + coCenter * (centerPosition - (Vector2)gang[i].transform.position).normalized + coAvoid * avoidFrom.normalized) * Time.deltaTime;
            newSpeed[i] = normalSpeed * newSpeed[i].normalized;
        }
        for(int i = 0; i < gang.Count; i++)
        {
            speed[i] = newSpeed[i];
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
        //各点の移動
        for (int i = 0; i < gang.Count; i++)
        {
            gang[i].transform.Translate(speed[i].x, speed[i].y, 0);
        }

    }
}
