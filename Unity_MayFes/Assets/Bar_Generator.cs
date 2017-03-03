using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar_Generator : MonoBehaviour
{

    public GameObject BarPrefab;
    public List<GameObject> list_bar = new List<GameObject>();
    int num;
    public float height = 100.0f;
    float wide = 0.1f;
    public Vector2 left = new Vector2(5.0f, 2.0f);

    // Use this for initialization
    void Start()
    {
        num = GameObject.Find("UIDirector").GetComponent<UI_Director>().bar_count;
        for (int i = 0; i < num; i++)
        {
            //プレハブを生成してリストで保持する。
            GameObject go = Instantiate(BarPrefab) as GameObject;
            go.transform.position = left + new Vector2(i * wide, 0);
            list_bar.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
