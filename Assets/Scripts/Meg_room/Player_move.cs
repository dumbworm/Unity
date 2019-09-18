using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[HideInInspector]
//可以使一个公有的变量在Inspector面板中隐藏起来
//[SerializeField]
//可以使一个私有的变量在Inspector面板中显示出来


public class Player_move : MonoBehaviour
{
    // Start is called before the first frame update
    //public int angle;
    //定义本体动画和body的引用
    [HideInInspector]   public Animator ani;
    [HideInInspector]   public bool IsGround;
    //小人是否接触地面，用来判断能否跳跃
    public SpriteRenderer other_player;
    private Player_room other_script;
    private Rigidbody2D rBody;
    //定义子物体精灵（megarea)子物体是个精灵
    public SpriteRenderer MegArea;

    public SpriteRenderer footleft, footright;
    public bool Isplayer1;
    //是不是第一个玩家，用来判断用哪一套按钮

    [HideInInspector] public int dirc;//这个变量表明N极（红）所对应方向，0123分别表示上右下左。

    void Start()
    {
        ani = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        IsGround = true;
        ani.SetBool("Isplayer1", Isplayer1?true:false);//选择玩家不同的动画
        other_script = other_player.GetComponent<Player_room>();
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

    // Update is called once per frame
    void Update()
    {
        Vector2 v = rBody.velocity;
        float speed = 2f;
        //小人移动   other_player：0上下，1左右。。。.dirc,0123上右下左
        if (other_script.dirc==0)//上下结构
        {
            if (dirc == 0)//向下运行
                v.y=-speed;
            else if (dirc == 2)//向上运行
                v.y=speed;
             
        }
        else if (other_script.dirc  == 1)//左右结构
        {
            if (dirc == 1)//向左行
                v.x=-speed;
            else if (dirc == 3)//向右运行
                v.x=speed;
        }
        rBody.velocity = v;


        //小人旋转：引力范围同时旋转
        if (Input.GetKeyDown(Isplayer1 ? KeyCode.DownArrow : KeyCode.S))
        {
            MegArea.transform.Rotate(Vector3.forward * (-90));
            Setedges();
        }

    }

    /// <summary>
    /// 根据小人旋转角度来确认小人所对应的方向
    /// </summary>
    /// <returns></returns>
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
