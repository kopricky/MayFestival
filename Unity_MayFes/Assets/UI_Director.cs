using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_Director : MonoBehaviour
{
    List<GameObject> list_gang, list_bar;
    float max_speed;
    float min_speed;
    float speed_range;
    float[] bar_value = new float[100];
    Slider[] slider = new Slider[100];
    // Use this for initialization
    void Start()
    {
        list_gang = GameObject.Find("GangGenerator").GetComponent<Gang_Generator>().list_gang;
        list_bar = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().list_bar;
        for (int i = 0; i < list_bar.Count; i++)
        {
            slider[i] = list_bar[i].GetComponent<Slider>();
        }
        min_speed = 0.0f;
        max_speed = 10.0f;
        speed_range = max_speed - min_speed;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < list_bar.Count; i++)
        {
            slider[i].value = 0.0f;
        }
        Debug.Log(list_gang[3].GetComponent<Rigidbody2D>().mass);
        for (int i = 0; i < list_gang.Count; i++)
        {
            float gang_speed = list_gang[i].GetComponent<Rigidbody2D>().velocity.magnitude;
            int index = (int)(gang_speed / speed_range * list_bar.Count);
            slider[index].value += 1.0f / list_gang.Count;
            slider[i].value += 1.0f / list_gang.Count;
        }
    }
}