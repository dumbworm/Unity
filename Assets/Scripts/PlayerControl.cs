using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[HideInInspector]
//可以使一个公有的变量在Inspector面板中隐藏起来
//[SerializeField]
//可以使一个私有的变量在Inspector面板中显示出来


public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    //public int angle;
    //定义本体动画和body的引用
    [HideInInspector]   public Animator ani;
    [HideInInspector]   public bool IsGround;
    //小人是否接触地面，用来判断能否跳跃
    [HideInInspector] public bool IsOnPlayer;//是否站在另一个小人上,判断跳跃
    public SpriteRenderer other_player;
    private Rigidbody2D rBody;
    //定义子物体精灵（megarea)子物体是个精灵
    public SpriteRenderer MegArea;
    private megarea_control meg_script;

    public SpriteRenderer footleft, footright;
    public bool Isplayer1;
    //是不是第一个玩家，用来判断用哪一套按钮
    private static float[] min_max_dis = {0.5f,4.45f };//物体最近和最远距离
    private static float[] gra_rate = { 4f,1f };//变快最近和最远时rate
    private static float[] rep_rate= { 0.1f, 1f };//变慢最近和最远时rate

    [HideInInspector] public int dirc;//这个变量表明N极（红）所对应方向，0123分别表示上右下左。

    //private AudioManager amr;
    //private AudioClip clip_move = Resources.Load<AudioClip>("Sound/移动");

    void Start()
    {
        ani = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        meg_script = MegArea.GetComponent<megarea_control>();
        IsGround = true;
        IsOnPlayer = false;
        ani.SetBool("Isplayer1", Isplayer1?true:false);//选择玩家不同的动画

        //如果是另一个小人，则换精灵(磁极，脚 ）
        if (!Isplayer1) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("MegArea", "player_1");
            dic.Add("foot_left", "player_13");
            dic.Add("foot_right", "player_14");

            Sprite[] spPokers = Resources.LoadAll<Sprite>("player");
            SpriteRenderer[] childs;
            childs = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer child in childs)
            {
                if (dic.ContainsKey(child.name))
                {
                    //child.sprite = spPokers.
                    foreach(Sprite sp in spPokers)
                    {
                        if (dic[child.name] == sp.name)
                        {
                            child.sprite = sp;
                            break;
                        }
                    }

                }
            }
            
        }
    }

    public void PlaySoundOne(string resource)
    {
        AudioClip clip = Resources.Load<AudioClip>(resource);
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        //小人移动
        float horizontal = Input.GetAxis(Isplayer1?"Horizontal": "Horizontal1");
        if (horizontal != 0)
        {
            //AudioClip clip_move = Resources.Load<AudioClip>("Sound/移动");
            //GetComponent<AudioSource>().PlayOneShot(clip_move);

            //当水平方向有磁力时，将影响相对速度。
            float rate = 1f;
            if (meg_script.direct == 2)//左右结构时会影响移动速度
            {
                Vector2 dis = other_player.transform.localPosition - transform.localPosition;
                bool fastorslow = true;//true快，false慢
                if (meg_script.force_type == 1)//吸力
                {
                    if (dis.x * horizontal > 0)//同方向变快
                    {
                        fastorslow = true;
                    }
                    else
                    {
                        fastorslow = false;
                    }
                }
                else if (meg_script.force_type == 2)//斥力
                {
                    if (dis.x * horizontal > 0)//同方向变慢
                    {
                        fastorslow = false;
                    }
                    else
                    {
                        fastorslow = true;
                    }
                }
                if (fastorslow)
                    rate = gra_rate[0] - (Mathf.Abs(dis.x) - min_max_dis[0]) * (gra_rate[0] - gra_rate[1]) / (min_max_dis[1] - min_max_dis[0]);
                else
                    rate = rep_rate[0] + (Mathf.Abs(dis.x) - min_max_dis[0]) * (rep_rate[1] - rep_rate[0]) / (min_max_dis[1] - min_max_dis[0]);

            }
 
            Vector2 v = rBody.velocity;
            v.x = horizontal *3f* rate;
            rBody.velocity = v;
            footleft.flipX= horizontal > 0 ? false : true;
            footright.flipX = horizontal > 0 ? false : true;
            ani.SetBool("Isrunleft", horizontal > 0 ? false:true);
            ani.SetBool("Isrunright", horizontal > 0 ? true : false);
        }
        else
        {
            ani.SetBool("Isrunleft", false);
            ani.SetBool("Isrunright", false);

        }

        //小人跳跃.站在地上，或者站在另一个小人头上并且另一个站地上
        if(Input.GetKeyDown(Isplayer1?KeyCode.UpArrow: KeyCode.W) &&(IsGround||(IsOnPlayer&&other_player.GetComponent<PlayerControl>().IsGround)))
        {
            PlaySoundOne("Sound/跳跃");
            //AudioClip clip = Resources.Load<AudioClip>("Sound/跳跃");
            //GetComponent<AudioSource>().PlayOneShot(clip);
             
            //2个小人处于上下结构时，跳跃要加力，否则力度太小
            if (meg_script.direct==1)   rBody.AddForce(Vector2.up * 480f);
            else                         rBody.AddForce(Vector2.up * 380f);
            ani.SetBool("Isjump", true);

        }
        //小人旋转：引力范围同时旋转
        if (Input.GetKeyDown(Isplayer1 ? KeyCode.DownArrow : KeyCode.S))
        {
            //AudioClip clip = Resources.Load<AudioClip>(Isplayer1 ? "Sound/拾取道具1" : "Sound/拾取道具2");
            PlaySoundOne("Sound/旋转");
            //AudioClip clip = Resources.Load<AudioClip>("Sound/旋转.wav");
            //GetComponent<AudioSource>().PlayOneShot(clip);


            MegArea.transform.Rotate(Vector3.forward * (-90));
            Setedges();
        }
    }
    void Setedges()
    {
        switch (MegArea.transform.localEulerAngles.z)
        {
            case 0:
                dirc = 0;
                break;
            case 90:
            case -270:
                dirc = 3;
                break;
            case 180:
            case -180:
                dirc = 2;
                break;
            case -90:
            case 270:
                dirc = 1;
                break;
        }
    }
}
