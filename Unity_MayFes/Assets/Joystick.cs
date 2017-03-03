using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour {
    Vector2 original_pos;
    Vector2 pre_pos;
	// Use this for initialization
	void Start () 
    {
	    original_pos = transform.position;
	}

    void OnMouseDrag()
    {
        Vector2 position = Input.mousePosition;
        pre_pos = Camera.main.ScreenToWorldPoint(position);
        transform.position = pre_pos;
        Debug.Log("OK");
    }

	// Update is called once per frame
	void Update () 
    {
	}
}
