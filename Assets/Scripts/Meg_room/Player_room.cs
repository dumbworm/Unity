using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[HideInInspector]
//可以使一个公有的变量在Inspector面板中隐藏起来
//[SerializeField]
//可以使一个私有的变量在Inspector面板中显示出来


public class Player_room : MonoBehaviour
{
    // Start is called before the first frame update
    //public int angle;
    //定义本体动画和body的引用
    [HideInInspector]   public Animator ani;
    public SpriteRenderer other_player;
    private Rigidbody2D rBody;
    //定义子物体精灵（megarea)子物体是个精灵
    public SpriteRenderer MegArea;
    private megarea_control meg_script;

    public SpriteRenderer footleft, footright;
    public bool Isplayer1;
    //是不是第一个玩家，用来判断用哪一套按钮
    [HideInInspector] public int dirc;//这个变量表明N极（红）所对应方向，01分别表示上下和左右结构。
    public Transform edges;//四个边上的物体
    private SpriteRenderer left_n, left_s, right_n, right_s, top_n, top_s, bottom_n, bottom_s;

    void Start()
    {
        ani = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        meg_script = MegArea.GetComponent<megarea_control>();
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
        //代码获取四个边共8个对象
        foreach(SpriteRenderer it in edges.GetComponentsInChildren<SpriteRenderer>())
        {
            switch (it.name)
            {
                case "left_n":
                    left_n = it;
                    break;
                case "left_s":
                    left_s = it;
                    break;
                case "right_n":
                    right_n = it;
                    break;
                case "right_s":
                    right_s = it;
                    break;
                case "top_n":
                    top_n = it;
                    break;
                case "top_s":
                    top_s = it;
                    break;
                case "bottom_n":
                    bottom_n = it;
                    break;
                case "bottom_s":
                    bottom_s = it;
                    break;
            }
        }
        Setedges();
    }

    // Update is called once per frame
    void Update()
    {
        //小人移动
        float horizontal = Input.GetAxis(Isplayer1?"Horizontal": "Horizontal1");
        if (horizontal != 0)
        {
            Vector2 v = rBody.velocity;
            v.x = horizontal *3f;
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

        //小人跳跃.
        if(Input.GetKeyDown(Isplayer1?KeyCode.UpArrow: KeyCode.W))
        {
            rBody.AddForce(Vector2.up * 380f);
            ani.SetBool("Isjump", true);

        }
        //小人旋转：引力范围同时旋转
        if (Input.GetKeyDown(Isplayer1 ? KeyCode.DownArrow : KeyCode.S))
        {
            MegArea.transform.Rotate(Vector3.forward * (-90));
            Setedges();//获取旋转，并调整四面的磁极
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
            case 180:
            case -180:
                dirc = 0;
                break;
            case 90:
            case -270:
            case -90:
            case 270:
                dirc = 1;
                break;
        }

        //dirc=0123分别表示 上右下左
        left_n.enabled = dirc == 1 ? true : false;
        left_s.enabled = dirc == 0 ? true : false;
        right_n.enabled = dirc == 1 ? true : false;
        right_s.enabled = dirc == 0 ? true : false;
        top_n.enabled = dirc == 0 ? true : false;
        top_s.enabled = dirc == 1 ? true : false;
        bottom_n.enabled = dirc == 0 ? true : false;
        bottom_s.enabled = dirc == 1 ? true : false;
    }
   
}
