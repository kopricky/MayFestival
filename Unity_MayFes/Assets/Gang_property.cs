using UnityEngine;
using System.Collections;

public class Gang_property : MonoBehaviour {
    float[] dir = new float[100];
    public Vector2 speed; //gangの速度ベクトル
    GameObject[] gangs;
    Vector2 newSpeed;
    
    Vector2 newPosition;
    int gang_N;
    public float coAverage; //平均の向きの影響度
    public float r_ave; //対象となるgangの範囲
    public float coCenter; //群れの中心へ向かう影響度
    public float r_cen; //対象となるgangの範囲
    public float coAvoid; //離れる向きの影響度
    public float r_avo; //近すぎかどうかの基準
    public float coEscape; //敵から離れる強さ
    public float r_esc; //敵を認識する範囲
    public float normalSpeed;
    float regulation; //速度制限がどれだけかかっているか
    public float r_inf; //スピードの影響を受ける範囲
    public float coInfluence; //スピードの影響を受ける影響度
    int count_ave;
    int count_cen;
    int count_avo;
    //ゲーム全体に関する情報
    GameController c;
    //周りへの影響度
    public float influence;
    Color color;
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
        speed.x = 3f;
        speed.y = 3f;
        color_map[0] = Color.white;
        color_map[1] = Color.cyan;
        color_map[2] = Color.yellow;
        color_map[3] = Color.red;
        influence = 0;
        if(Random.value > 0.9)
        {
            influence = 10;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        regulation = Mathf.Infinity;
	}
	
    //暴走族が警察と衝突したとき
    void OnTriggerEnter2D(Collider2D col)
    {
        //制限速度オーバーなら速度を遅くし、違反回数を表す色を変更する
        if (true/*col.gameObject.name == "police"*/ /*&& speed.magnitude > speed_limit*/)
        {
            
            //speed *= 0.8f;
            color_idx++;
            GetComponent<SpriteRenderer>().color = color_map[color_idx%4];
            regulation = 2.0F;
        }
        
    }
    private Vector2 CalcFlockDirection() {
        Vector2 averageSpeed;
        Vector2 centerPosition;
        Vector2 avoidFrom;
        Vector2 avoidPolice;

        gangs = GameObject.FindGameObjectsWithTag("gang");
        for (int i = 0; i < gangs.Length; i++)
        {
            dir[i] = (gangs[i].transform.position - transform.position).magnitude;
        }
        centerPosition = Vector2.zero;
        averageSpeed = Vector2.zero;
        avoidFrom = Vector2.zero;

        Transform police = GameObject.FindWithTag("police").transform;
        avoidPolice = transform.position - police.position;
        if(avoidPolice.magnitude > r_esc)
        {
            avoidPolice = Vector2.zero;
        }

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

        return speed + (coEscape * avoidPolice.normalized + coAverage * averageSpeed.normalized + coCenter * (centerPosition - (Vector2)transform.position).normalized + coAvoid * avoidFrom.normalized) * Time.deltaTime;

    }
    private float CalcSpeedMagnitude()
    {
        float magnitude;
        float denominator;
        denominator = 0;
        magnitude = 0;
        for (int i = 0;i< Mathf.Min(gangs.Length,dir.Length); i++)
        {
            if(dir[i] < r_inf && gangs[i] != null)
            {
                Gang_property g = gangs[i].GetComponent<Gang_property>();
                magnitude +=  g.influence * g.speed.magnitude;
                denominator += g.influence;
            }
        }
        if(denominator == 0)
        {
            return speed.magnitude;
        }
        magnitude /= denominator;
        return speed.magnitude + (magnitude - speed.magnitude) / (1 + influence);
        //return speed.magnitude + coInfluence * (magnitude - speed.magnitude) * Time.deltaTime;
    }
    private void BoundaryTreatment()
    {
        //境界処理
        if (newPosition.y > c.upperBound)
        {
            newSpeed.y *= -1;
            newPosition.y = c.upperBound - (newPosition.y - c.upperBound);
        }
        if (newPosition.y < c.lowerBound)
        {
            newSpeed.y *= -1;
            newPosition.y = c.lowerBound - (newPosition.y - c.lowerBound);
        }
        if (newPosition.x < c.leftBound)
        {
            newSpeed.x *= -1;
            newPosition.x = c.leftBound - (newPosition.x - c.leftBound);
        }
        if (newPosition.x > c.rightBound)
        {
            newSpeed.x *= -1;
            newPosition.x = c.rightBound - (newPosition.x - c.rightBound);
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
        newSpeed = CalcFlockDirection().normalized;
        float speedMagnitude = Mathf.Min(regulation, CalcSpeedMagnitude());
        if (Random.value > 0.99F)
        {
            regulation = Mathf.Infinity;
        }
        if(Random.value > 0.999F)
        {
            speedMagnitude = 5F;
        }
        newSpeed *= speedMagnitude;
        newPosition = (Vector2)transform.position + newSpeed * Time.deltaTime;
        BoundaryTreatment();
    }
    void LateUpdate()
    {
        speed = newSpeed;
        //各点の移動
        transform.position = newPosition;
    }
}
