using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar_Generator : MonoBehaviour
{

    public GameObject BarPrefab;
    public List<GameObject> list_bar = new List<GameObject>();
    int bar_count;
    //バーの高さの最大値と幅
    public float height;
    float wide;
    //左端のバーの位置
    public Vector2 left = new Vector2(5.0f, 2.0f);

    // Use this for initialization
    void Awake()
    {
        bar_count = 29;
        height = 10.0f;
        wide = 0.1f;
        for (int i = 0; i < bar_count; i++)
        {
            //プレハブを生成してリストで保持する。
            GameObject go = Instantiate(BarPrefab) as GameObject;
            go.transform.position = left + new Vector2(i * wide, 0);
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
