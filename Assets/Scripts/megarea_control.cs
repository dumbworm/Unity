using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megarea_control : MonoBehaviour
{
    // Start is called before the first frame update
    //磁极的2个质心,CE为整个磁铁的质心
    public GameObject NP;
    public GameObject SP;
    public GameObject CE;
    //计算磁性力大小的常量,strength是力大小常量。
    public float Maxstrength_H, Maxstrength_V;//分别分开设置上下，和左右结构的磁力大小
    public float Minstrength_H, Minstrength_V;//设置最小磁力大小
    private float strength_min, other_strength_min, strength_max, other_strength_max;//本体力度和对方力度
    [HideInInspector] public int direct;//2个物体相对方向，1均为上下结构，2均为左右结构，0其它方式，
    [HideInInspector] public int force_type;//力的类型，0无任何磁力，1吸力，2斥力。注：有磁力影响时，才有相对方向和力类型
    void Start()
    {
        direct = 0;
        force_type = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //小人触发引力区域
    private void OnTriggerStay2D(Collider2D collision)
    {
        //非磁性物质引力模板
        if (collision.gameObject.tag == "???")//其它非磁物质，总吸引
        {
            Rigidbody2D other = collision.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D rBody = GetComponentInParent<Rigidbody2D>();
            //int other_angel = collision.GetComponent<PlayerControl>().angle;
            Vector2 r_n = collision.gameObject.transform.position - NP.transform.position;
            Vector2 r_s = collision.gameObject.transform.position - SP.transform.position;
            float mu = Time.fixedDeltaTime * Maxstrength_H * other.mass;
            Vector2 f_n = (r_n.normalized) * mu /
                (Mathf.Pow(Mathf.Max(r_n.magnitude, 0.2f), 3));
            Vector2 f_s = (r_s.normalized) * mu /
                (Mathf.Pow(Mathf.Max(r_s.magnitude, 0.2f), 3));
            other.AddForce(-1f * f_n);
            other.AddForce(-1f * f_s);
            rBody.AddForceAtPosition(1f * f_n, NP.transform.position);
            rBody.AddForceAtPosition(1f * f_s, SP.transform.position);
        }
        //2个玩家相互吸引和排斥，重写磁力，改为线性磁力20190718
        //第4次调整模板20190718
        else if (collision.gameObject.tag=="Player")// collision.gameObject.tag == "Player")//与其它玩家，有相互吸引和排斥。
        {
            //取父物体的rdgidbody用于施力
            Rigidbody2D other = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            //取对象的strength量及np,sp方位
            megarea_control other_script = collision.gameObject.GetComponentInChildren<megarea_control>();
            GameObject other_NP = other_script.NP;
            GameObject other_SP = other_script.SP;
            GameObject other_CE = other_script.CE;
            Vector2 other_dis = other_NP.transform.position - other_SP.transform.position;
            Vector2 me_dis = NP.transform.position - SP.transform.position;
            //4组距离向量
            //当2个小人站在单方向平台上时，计算它们自身的me_dis并不能==0，
            //而是无限接近于0，不能使用==0来判断  20190719
            if (Mathf.Abs(other_dis.x) < 0.1f && Mathf.Abs(me_dis.x) < 0.1f) direct = 1;//上下结构
            else if (Mathf.Abs(other_dis.y) < 0.1f && Mathf.Abs(me_dis.y) < 0.1f) direct = 2;//左右结构
            else { direct = 0;force_type = 0; }
            if (direct==1 ||direct==2)//同为上下结构或左右结构时产生力
            {

                Vector2[] dis = new Vector2[4];// r_n_n, r_s_s, r_n_s, r_s_n;
                if (direct==1)
                {//上下结构
                    //上下结构中，如果2个角色减向量在x轴投影大于y轴投影，则磁力不成立。
                    //主要防止2个角色上下结构但并列排放时产生力作用
                    Vector2 ce_ver = other_CE.transform.position - CE.transform.position;
                    if (Mathf.Abs(ce_ver.x) > 0.75*Mathf.Abs(ce_ver.y)) return;//不成立时返回。
                    dis[0] = new Vector2(0f, other_NP.transform.position.y - NP.transform.position.y);
                    dis[1] = new Vector2(0f, other_SP.transform.position.y - SP.transform.position.y);
                    dis[2] = new Vector2(0f, other_SP.transform.position.y - NP.transform.position.y);
                    dis[3] = new Vector2(0f, other_NP.transform.position.y - SP.transform.position.y);
                    strength_max = Maxstrength_V;
                    other_strength_max = other_script.Maxstrength_V;
                    strength_min = Minstrength_V;
                    other_strength_min = other_script.Minstrength_V;
                }
                else if (direct==2)//左右结构
                {
                    //同上下结构
                    Vector2 ce_ver = other_CE.transform.position - CE.transform.position;
                    if (Mathf.Abs(ce_ver.y) > 0.75*Mathf.Abs(ce_ver.x)) return;//不成立时返回。
                    dis[0] = new Vector2(other_NP.transform.position.x - NP.transform.position.x, 0f);
                    dis[1] = new Vector2(other_SP.transform.position.x - SP.transform.position.x, 0f);
                    dis[2] = new Vector2(other_SP.transform.position.x - NP.transform.position.x, 0f);
                    dis[3] = new Vector2(other_NP.transform.position.x - SP.transform.position.x, 0f);
                    strength_max = Maxstrength_H;
                    other_strength_max = other_script.Maxstrength_H;
                    strength_min = Minstrength_H;
                    other_strength_min = other_script.Minstrength_H;
                }
                //从4组向量中找出距离最小的向量进而施力，将其余向量置0
                float minn = 0x3f3f3f3f;
                int pos = 0;//取向量距离最小的向量序号
                for (int i = 0; i < 4; i++)
                    if (minn > dis[i].sqrMagnitude)
                    {
                        pos = i;
                        minn = dis[i].sqrMagnitude;
                    }
                Vector2[] f_res = new Vector2[4];// r_n_n, r_s_s, r_n_s, r_s_n;各个向量的力大小
                float mu_max = strength_max + other_strength_max;// *Time.fixedDeltaTime ;
                float mu_min = strength_min + other_strength_min;
                for (int i = 0; i < 4; i++)
                {
                    if (i == pos) {
                        //线性磁力，在0.5到3.5的距离内，磁力从mu_max到mu_min之间线性递减
                        //mu为约定的磁铁挨在一起的最大磁力，0.5为约定的磁铁靠近时最近的距离，5f为约定的磁铁最远距离为3.5时的磁力值
                        f_res[i] = (dis[i].normalized) * Mathf.Max(0f,mu_max - (dis[i].magnitude-0.5f)*(mu_max - mu_min) / (3.5f - 0.5f));
                        //Debug.Log("time:" + Time.fixedDeltaTime);
                       // Debug.Log("str:"+ strength_max + strength_max);
                       
                        if (i < 2) force_type = 2;//0和1 时，斥力，否则引力。
                        else force_type = 1;
                    } else
                        f_res[i] = new Vector2(0f, 0f);
                }
                //Debug.Log(f_res[0]+ f_res[1]+ f_res[2]+ f_res[3]);
                //4组力施力
                other.AddForceAtPosition(1f * f_res[0], other_CE.transform.position);
                other.AddForceAtPosition(1f * f_res[1], other_CE.transform.position);
                other.AddForceAtPosition(-1f * f_res[2], other_CE.transform.position);
                other.AddForceAtPosition(-1f * f_res[3], other_CE.transform.position);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //方向和力类型清空
            direct = 0;
            force_type = 0;
        }
    }
}



/*//原始版本磁力
        //2个玩家相互吸引和排斥
        else if (collision.gameObject.tag == "Player")//与其它玩家，有相互吸引和排斥。
        {
            //取父物体的rdgidbody用于施力
            Rigidbody2D other = collision.gameObject.GetComponentInParent<Rigidbody2D>();
//取对象的strength量及np,sp方位
megarea_control other_script = collision.gameObject.GetComponentInChildren<megarea_control>();
GameObject other_NP = other_script.NP;
GameObject other_SP = other_script.SP;
GameObject other_CE = other_script.CE;
Vector2 other_dis = other_NP.transform.position - other_SP.transform.position;
Vector2 me_dis = NP.transform.position - SP.transform.position;
            //4组距离向量
            if ((other_dis.x == 0 && me_dis.x == 0) || (other_dis.y == 0 && me_dis.y == 0))//同为上下结构或左右结构时产生力
            {
                float min_dis = 0.5f;//定义一个最小距离常量，防止过近时力太大，当然此种操作仅在上下结构时使用
Vector2[] dis = new Vector2[4];// r_n_n, r_s_s, r_n_s, r_s_n;
                if (other_dis.x == 0 && me_dis.x == 0)
                {//上下结构
                    dis[0] = new Vector2(0f, other_NP.transform.position.y - NP.transform.position.y);
dis[1] = new Vector2(0f, other_SP.transform.position.y - SP.transform.position.y);
dis[2] = new Vector2(0f, other_SP.transform.position.y - NP.transform.position.y);
dis[3] = new Vector2(0f, other_NP.transform.position.y - SP.transform.position.y);
strength = strength_V;
                    other_strength = other_script.strength_V;
                    min_dis = 1.8f;
                    is_V_and_V = true;
                }
                else if (other_dis.y == 0 && me_dis.y == 0)//左右结构
                {
                    dis[0] = new Vector2(other_NP.transform.position.x - NP.transform.position.x, 0f);
dis[1] = new Vector2(other_SP.transform.position.x - SP.transform.position.x, 0f);
dis[2] = new Vector2(other_SP.transform.position.x - NP.transform.position.x, 0f);
dis[3] = new Vector2(other_NP.transform.position.x - SP.transform.position.x, 0f);
strength = strength_H;
                    other_strength = other_script.strength_H;
                    min_dis = 0.5f;
                }
                //从4组向量中找出距离最小的向量进而施力，将其余向量置0
                float minn = 0x3f3f3f3f;
int pos = 0;//取向量距离最小的向量序号
                for (int i = 0; i< 4; i++)
                    if (minn > dis[i].sqrMagnitude)
                    {
                        pos = i;
                        minn = dis[i].sqrMagnitude;
                    }
                Vector2[] f_res = new Vector2[4];// r_n_n, r_s_s, r_n_s, r_s_n;各个向量的力大小
float mu = Time.fixedDeltaTime * mu0 * (strength + other_strength);
                for (int i = 0; i< 4; i++)
                {
                    if (i == pos) {
                        f_res[i] = (dis[i].normalized) * mu / Mathf.Pow(Mathf.Max(dis[i].magnitude,min_dis), 1.5f);//(dis[i].normalized) * mu / (Mathf.Pow(Mathf.Max(r_n_n.magnitude, 0.5f), 2))
                        //f_res[i] = (dis[i].normalized) * (18f - (dis[i].magnitude-0.5f)*(18f - 5f) / (6f - 0.5f));//(Mathf.Max(0f, 22f - 3f * dis[i].magnitude));
                        Debug.Log(dis[i].magnitude);
                        Debug.Log(f_res[i]);
                    } else
                        f_res[i] = new Vector2(0f, 0f);
                }
                //Debug.Log(f_res[0]+ f_res[1]+ f_res[2]+ f_res[3]);
                //4组力施力
                other.AddForceAtPosition(1f * f_res[0], other_CE.transform.position);
                other.AddForceAtPosition(1f * f_res[1], other_CE.transform.position);
                other.AddForceAtPosition(-1f * f_res[2], other_CE.transform.position);
                other.AddForceAtPosition(-1f * f_res[3], other_CE.transform.position);
            }
        }
    */


/*//第二2个版本，改为了线性磁力
 * 
         else if (collision.gameObject.tag == "Player")//与其它玩家，有相互吸引和排斥。
    {
        //取父物体的rdgidbody用于施力
        Rigidbody2D other = collision.gameObject.GetComponentInParent<Rigidbody2D>();
        //取对象的strength量及np,sp方位
        megarea_control other_script = collision.gameObject.GetComponentInChildren<megarea_control>();
        GameObject other_NP = other_script.NP;
        GameObject other_SP = other_script.SP;
        GameObject other_CE = other_script.CE;
        Vector2 other_dis = other_NP.transform.position - other_SP.transform.position;
        Vector2 me_dis = NP.transform.position - SP.transform.position;
        //4组距离向量
        if ((other_dis.x == 0 && me_dis.x == 0) || (other_dis.y == 0 && me_dis.y == 0))//同为上下结构或左右结构时产生力
        {
            Vector2[] dis = new Vector2[4];// r_n_n, r_s_s, r_n_s, r_s_n;
            if (other_dis.x == 0 && me_dis.x == 0)
            {//上下结构
                dis[0] = new Vector2(0f, other_NP.transform.position.y - NP.transform.position.y);
                dis[1] = new Vector2(0f, other_SP.transform.position.y - SP.transform.position.y);
                dis[2] = new Vector2(0f, other_SP.transform.position.y - NP.transform.position.y);
                dis[3] = new Vector2(0f, other_NP.transform.position.y - SP.transform.position.y);
                strength = Maxstrength_V;
                other_strength = other_script.Maxstrength_V;
                is_V_and_V = true;
            }
            else if (other_dis.y == 0 && me_dis.y == 0)//左右结构
            {
                dis[0] = new Vector2(other_NP.transform.position.x - NP.transform.position.x, 0f);
                dis[1] = new Vector2(other_SP.transform.position.x - SP.transform.position.x, 0f);
                dis[2] = new Vector2(other_SP.transform.position.x - NP.transform.position.x, 0f);
                dis[3] = new Vector2(other_NP.transform.position.x - SP.transform.position.x, 0f);
                strength = Maxstrength_H;
                other_strength = other_script.Maxstrength_H;
            }
            //从4组向量中找出距离最小的向量进而施力，将其余向量置0
            float minn = 0x3f3f3f3f;
            int pos = 0;//取向量距离最小的向量序号
            for (int i = 0; i < 4; i++)
                if (minn > dis[i].sqrMagnitude)
                {
                    pos = i;
                    minn = dis[i].sqrMagnitude;
                }
            Vector2[] f_res = new Vector2[4];// r_n_n, r_s_s, r_n_s, r_s_n;各个向量的力大小
            float mu = strength + other_strength;// *Time.fixedDeltaTime ;
            for (int i = 0; i < 4; i++)
            {
                if (i == pos) {
                    //线性磁力，在0.5到3.5的距离内，磁力从mu到5f之间线性递减
                    //mu为约定的磁铁挨在一起的最大磁力，0.5为约定的磁铁靠近时最近的距离，5f为约定的磁铁最远距离为3.5时的磁力值
                    f_res[i] = (dis[i].normalized) * Mathf.Max(0f,mu - (dis[i].magnitude-0.5f)*(mu - 5f) / (3.5f - 0.5f));
                    //Debug.Log("time:" + Time.fixedDeltaTime);
                    //Debug.Log("str:"+strength + other_strength);
                    //Debug.Log(mu);
                    //Debug.Log(dis[i].magnitude);
                    //Debug.Log(f_res[i]);
                } else
                    f_res[i] = new Vector2(0f, 0f);
            }
            //Debug.Log(f_res[0]+ f_res[1]+ f_res[2]+ f_res[3]);
            //4组力施力
            other.AddForceAtPosition(1f * f_res[0], other_CE.transform.position);
            other.AddForceAtPosition(1f * f_res[1], other_CE.transform.position);
            other.AddForceAtPosition(-1f * f_res[2], other_CE.transform.position);
            other.AddForceAtPosition(-1f * f_res[3], other_CE.transform.position);
        }
    }
 * 
 */
