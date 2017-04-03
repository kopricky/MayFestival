using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar_Generator : MonoBehaviour
{

    public GameObject BarPrefab;
    public GameObject BarPrefab2;
    public List<GameObject> list_bar = new List<GameObject>();
    public int bar_count;
    //左端のバーの位置
    GameObject layout;
    public int over;

    // Use this for initialization
    void Awake()
    {
        layout = GameObject.Find("Bar_Layout");
        float min_speed = GameObject.Find("UIDirector").GetComponent<UI_Director>().min_speed;
        float max_speed = GameObject.Find("UIDirector").GetComponent<UI_Director>().max_speed;
        float speed_range = max_speed - min_speed;
        float speed_limit = GameObject.Find("GameController").GetComponent<GameController>().speed_limit;
        over = (int)((speed_limit - min_speed) / speed_range * bar_count);
        for (int i = 0; i <over; i++)
        {
            //プレハブを生成してリストで保持する。
            GameObject go = Instantiate(BarPrefab) as GameObject;
            go.transform.parent = layout.transform;
            list_bar.Add(go);
        }
        for (int i = over; i < bar_count; i++)
        {
            GameObject go = Instantiate(BarPrefab2) as GameObject;
            go.transform.parent = layout.transform;
            list_bar.Add(go);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
