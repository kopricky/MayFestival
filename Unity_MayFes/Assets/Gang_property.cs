using UnityEngine;
using System.Collections;

public class Gang_property : MonoBehaviour {
    float[] dir = new float[100];
    public Vector2 speed; //gangの速度ベクトル
    GameObject[] gangs;
    Vector2 newSpeed;
    Vector2 averageSpeed;
    Vector2 centerPosition;
    Vector2 avoidFrom;
    Vector2 newPosition;
    int gang_N;
    public float coAverage; //平均の向きの影響度
    public float r_ave; //対象となるgangの範囲
    public float coCenter; //群れの中心へ向かう影響度
    public float r_cen; //対象となるgangの範囲
    public float coAvoid; //離れる向きの影響度
    public float r_avo; //近すぎかどうかの基準
    public float normalSpeed;
    int count_ave;
    int count_cen;
    int count_avo;
    //ゲーム全体に関する情報
    GameController c;
    //周りへの影響度
    public float influence;
    //違反回数を表す色
    public Color color;
    public 
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
        c = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gangs = new GameObject[100];
        speed.x = 0.1f;
        speed.y = 0.1f;
        color = GetComponent<SpriteRenderer>().color;
        color_map[0] = Color.white;
        color_map[1] = Color.cyan;
        color_map[2] = Color.yellow;
        color_map[3] = Color.red;
	}
	
    //暴走族が警察と衝突したとき
    void OnCollisionEnter(Collision col)
    {
        //制限速度オーバーなら速度を遅くし、違反回数を表す色を変更する
        if (col.gameObject.name == "Police" && speed.magnitude > speed_limit)
        {
            speed *= 0.8f; //要変更
            color_idx++;
            color = color_map[color_idx];
        }
    }
	// Update is called once per frame
	void Update () 
    {
        gangs = GameObject.FindGameObjectsWithTag("gang");
        for(int i=0;i < gangs.Length;i++)
        {
            dir[i]= (gangs[i].transform.position - transform.position).magnitude;
        }
        centerPosition = Vector2.zero;
        averageSpeed = Vector2.zero;
        avoidFrom = Vector2.zero;
        count_cen = 0;
        count_ave = 0;
        count_avo = 0;
        
        for (int j = 0; j < gangs.Length; j++)
        {
            if (gangs[j] == this) { continue; }
            if (dir[j] < r_avo)
            {
                avoidFrom += (Vector2)(gangs[j].transform.position - transform.position).normalized;
                count_avo++;
            }
            else {
                if (dir[j] < r_ave)
                {
                    averageSpeed += gangs[j].GetComponent<Gang_property>().speed;
                    count_ave++;
                    if (dir[j] < r_cen)
                    {
                        centerPosition += (Vector2)gangs[j].transform.position;
                        count_cen++;
                    }
                }
            }
        }
        centerPosition /= count_cen;

        newSpeed = speed + (coAverage * averageSpeed.normalized + coCenter * (centerPosition - (Vector2)transform.position).normalized + coAvoid * avoidFrom.normalized) * Time.deltaTime;
        newSpeed = normalSpeed * newSpeed.normalized;
        newPosition = (Vector2)transform.position + newSpeed * Time.deltaTime;
        //境界処理
        if (newPosition.y > c.upperBound)
        {
            newSpeed.y *= -1;
            newPosition.y = c.upperBound - (newPosition.y - c.upperBound);
        }
        if(newPosition.y < c.lowerBound)
        {
            newSpeed.y *= -1;
            newPosition.y = c.lowerBound - (newPosition.y - c.lowerBound);
        }
        if(newPosition.x < c.leftBound)
        {
            newSpeed.x *= -1;
            newPosition.x = c.leftBound - (newPosition.x - c.leftBound);
        }
        if(newPosition.x > c.rightBound)
        {
            newSpeed.x *= -1;
            newPosition.x = c.rightBound - (newPosition.x - c.rightBound);
        }
    }
    void LateUpdate()
    {
        speed = newSpeed;
        //各点の移動
        transform.position = newPosition;
    }
}
