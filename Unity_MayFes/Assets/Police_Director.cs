using UnityEngine;
using System.Collections;

public class Police_Director : MonoBehaviour {
    public GameObject joystick;
    public float speed;
    GameController c;
    Vector2 dir;
    Joystick j;
	// Use this for initialization
	void Start () 
    {
        c = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        j = joystick.GetComponent<Joystick>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        dir = j.tellDirection().normalized;
	    if(dir.magnitude != 0)
        {
            transform.position += speed * (Vector3)dir * Time.deltaTime;
        }
        //境界処理
        float x = Mathf.Clamp(transform.position.x, c.leftBound, c.rightBound);
        float y = Mathf.Clamp(transform.position.y, c.lowerBound, c.upperBound);
        transform.position = new Vector2(x, y);
        //向きを進行方向にする
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 30;//defaultの向きが30度傾いているので
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
