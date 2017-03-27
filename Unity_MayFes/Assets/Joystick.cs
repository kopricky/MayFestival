using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour {
    //初期位置
    Vector2 original_pos;
    Vector2 pre_pos;
    float move_limit_radius = 0.6f;
	// Use this for initialization
	void Start () 
    {
	    original_pos = transform.position;
	}

    public Vector2 tellDirection()
    {
        return (Vector2)transform.position - original_pos;
    }

    //マウスでドラッグしている間、ドラッグした場所にスティック(円)の中心が移動
    void OnMouseDrag()
    {
        Vector2 position = Input.mousePosition;
        pre_pos = Camera.main.ScreenToWorldPoint(position);
        //位置の変位
        Vector2 move = pre_pos - original_pos;
        //スティックがmove_limit_radiusを超えて移動しないように制限
        if (move.magnitude > move_limit_radius)
        {
            pre_pos = original_pos + move_limit_radius * move / move.magnitude;
        }
        transform.position = pre_pos;
    }

    //マウスのドラッグをやめたときにスティックを初期位置に戻す
    void OnMouseUp()
    {
        transform.position = original_pos;
    }

	// Update is called once per frame
	void Update () 
    {
	}
}
