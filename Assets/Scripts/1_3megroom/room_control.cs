using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_control : MonoBehaviour
{
    // Start is called before the first frame update、

    [HideInInspector] public int dirc;//这个变量表明N极（红）所对应方向，0 1 -1分别表示没有，上下和左右结构。
    //改由系统 自动控制，将dirc=0去掉了。
    private SpriteRenderer left_n, left_s, right_n, right_s, top_n, top_s, bottom_n, bottom_s;
    //public bool Isplayer1;
    //是不是第一个玩家，用来判断用哪一套按钮


    void Start()
    {
        dirc = 0;
        //代码获取四个边共8个对象
        foreach (SpriteRenderer it in GetComponentsInChildren<SpriteRenderer>())
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
        InvokeRepeating("RoundDir", 2, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RoundDir()
    {
        //float i = Random.Range(-1f, 1f);
        dirc = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        Setedges();//调整四面的磁极
    }




    /// <summary>
    /// 根据小人旋转角度来确认小人所对应的方向
    /// </summary>
    /// <returns></returns>
    void Setedges()
    {
        //dirc=01分别表示上下和左右结构
        left_n.enabled = dirc == -1 ? true : false;
        left_s.enabled = dirc == 1 ? true : false;
        right_n.enabled = dirc == -1 ? true : false;
        right_s.enabled = dirc == 1 ? true : false;
        top_n.enabled = dirc == 1 ? true : false;
        top_s.enabled = dirc == -1 ? true : false;
        bottom_n.enabled = dirc == 1 ? true : false;
        bottom_s.enabled = dirc == -1 ? true : false;
    }
}
