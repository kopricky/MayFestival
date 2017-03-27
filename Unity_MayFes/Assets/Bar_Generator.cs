using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar_Generator : MonoBehaviour
{

    public GameObject BarPrefab;
    public List<GameObject> list_bar = new List<GameObject>();
    int bar_count;
    //左端のバーの位置
    GameObject layout;

    // Use this for initialization
    void Awake()
    {
        layout = GameObject.Find("Bar_Layout");
        bar_count = 29;
        for (int i = 0; i < bar_count; i++)
        {
            //プレハブを生成してリストで保持する。
            GameObject go = Instantiate(BarPrefab) as GameObject;
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
