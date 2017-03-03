using UnityEngine;
using System.Collections;

public class Gang_property : MonoBehaviour {
    public Rigidbody2D rb;
    //周りへの影響度
    public float influence;
    //違反回数を表す色
    public Color color;
    //現在の色が白なら０、水色なら１、黄色なら２、赤色なら３
    int color_idx;
    Color[] color_map = new Color[4];
    //速度の上界
    float speed_max = 0.2f;
    //速度の下界
    float speed_min = 0.01f;
    //制限速度
    float speed_limit = 0.1f;
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        color = GetComponent<SpriteRenderer>().color;
        color_map[0] = Color.white;
        color_map[1] = Color.cyan;
        color_map[2] = Color.yellow;
        color_map[3] = Color.red;
	}
	
    //暴走族が警察と衝突したとき
    void OnCollisionEnter(Collision col)
    {
        float pre_speed = rb.velocity.magnitude;
        //制限速度オーバーなら速度を遅くし、違反回数を表す色を変更する
        if (col.gameObject.name == "Police" && pre_speed > speed_limit)
        {
            rb.velocity *= 0.8f;
            color = color_map[(color_idx + 1) % 4];
        }
    }
	// Update is called once per frame
	void Update () 
    {
	}
}
