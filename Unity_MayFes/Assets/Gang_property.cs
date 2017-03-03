using UnityEngine;
using System.Collections;

public class Gang_property : MonoBehaviour {
    public Rigidbody2D rb;
    public float influence;
    public Color color;
    Color[] color_map = new Color[4];
    float speed_max=0.2f;
    float speed_min=0.01f;
    float speed_limit=0.1f;
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
	
	// Update is called once per frame
	void Update () 
    {
        float pre_speed = rb.velocity.magnitude;
        if (pre_speed > speed_limit)
        {
        }
	}
}
