using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_Director : MonoBehaviour
{
    List<GameObject> list_gang, list_bar;
    float speed_range;
    float[] bar_value = new float[100];
    Slider[] slider = new Slider[100];
    float speed_limit;
    public float min_speed;
    public float max_speed;
    int over;
    ColorBlock rb;
    // Use this for initialization
    void Start()
    {
        speed_range = max_speed - min_speed;
        list_gang = GameObject.Find("GangGenerator").GetComponent<Gang_Generator>().list_gang;
        list_bar = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().list_bar;
        for (int i = 0; i < list_bar.Count; i++)
        {
            slider[i] = list_bar[i].GetComponent<Slider>();
        }
        speed_limit = GameObject.Find("GameController").GetComponent<GameController>().speed_limit;
        over = GameObject.Find("BarGenerator").GetComponent<Bar_Generator>().over;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < list_bar.Count; i++)
        {
            slider[i].value = 0.0f;
        }
        for (int i = 0; i < list_gang.Count; i++)
        {
            float gang_speed = list_gang[i].GetComponent<Gang_property>().speed.magnitude;
            int index = (int)((gang_speed-min_speed) / speed_range * list_bar.Count);
            if (index >= list_bar.Count)
            {
                index = list_bar.Count-1;
            }
            slider[index].value += 1.0f / list_gang.Count;
        }
    }
}