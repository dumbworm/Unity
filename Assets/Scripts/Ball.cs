using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    public float speed;
    private int score_left = 0;
    private int score_right = 0;
    private float timer = 5;//计时器
    private bool IsBlue;
    private bool isrun = false;//球是否在运行

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("countdown", 1, 1);//每隔一秒调用一次timer减
    }

    // Update is called once per frame
    void Update()
    {
        if (!isrun && timer <= 0)//发球
        {
            if (score_left >= 5 || score_right >= 5)//开始新的一局
            {
                score_left = 0;
                score_right = 0;
            }
            startball();
            CancelInvoke();
            timer = 5;
        }

    }
    void OnGUI()//"<color=#00ff00><size=30>"+"aaa"+"</size></color>
    {
        GUI.Label(new Rect(400, 100, 300, 300), "<color=#000000><size=20><b>" + "  Left Score: " + score_left.ToString() + "</b></size></color>");
        GUI.Label(new Rect(400, 150, 300, 300), "<color=#000000><size=20><b>" + "Right Score: " + score_right.ToString() + "</b></size></color>");
        if (!isrun) { GUI.Label(new Rect(440, 200, 300, 300), "<color=#000000><size=20><b>" + "  倒计时: " + timer.ToString() + "</b></size></color>"); }
        if (score_left >= 5 || score_right >= 5)
        {
            string str;
            str = score_left > score_right ? "左边胜利！" : "右边胜利！";
            GUI.Label(new Rect(400, 250, 300, 300), "<color=#000000><size=40><b>" + str + "</b></size></color>");

        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.transform.tag == "Left" || collision.transform.tag == "Right")
        {
            MoveRacket other = collision.gameObject.GetComponent<MoveRacket>();
            if (other.IsBlue != IsBlue)
            {
                timer = 5;
                isrun = false;
               // GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (collision.transform.tag == "Left") score_right++;
                else score_left++;
                InvokeRepeating("countdown", 1, 1);
            }
        }
        else if (collision.transform.tag == "door_left" || collision.transform.tag == "door_right")
        {
            timer = 5;
            isrun = false;
           // GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (collision.transform.tag == "door_left") score_right++;
            else score_left++;
            InvokeRepeating("countdown", 1, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "MagicBox")
        {
            float i = Random.Range(0f, 9f);
            if (i < 1)
            {//倍速2s

                Vector2 verb = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().velocity = verb * 1.5f;
                //Invoke("recover", 2);
            }
            if (i >= 1 && i < 4)
            {//变色
                GetComponent<SpriteRenderer>().material.color = IsBlue ? Color.red : Color.blue;
                IsBlue = !IsBlue;
            }
            if (i >= 4 && i < 7)
            {//变向
                Vector2 dir = new Vector2(Random.Range(-0.5f, 0.5f) > 0 ? 1f : -1f, Random.Range(-0.6f, 0.6f));
                GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
            }
        }
    }

    public void startball()//生成一个发球
    {
        GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().material.color = Color.red;
        IsBlue = false;
        Vector2 dir = new Vector2(Random.Range(-0.5f, 0.5f) > 0 ? 1f : -1f, Random.Range(-0.6f, 0.6f));
        GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
        isrun = true;
    }
    public void countdown() { timer--; }
    //public void recover() { GetComponent<Rigidbody2D>().velocity /= 2; }
}
