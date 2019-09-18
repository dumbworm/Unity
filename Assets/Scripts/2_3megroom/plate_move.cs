using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate_move : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lose;
    private Transform son;
    public bool ishinder;//是否是碰撞即销毁的障碍
    private bool moveToLeft = true;
    private float speed = 2;
    private float[] pos1 = { -0.5f,4f };
    private float[] pos2 = { 10.5f, 15.8f };
    private float[] pos3 = { 4.68f, 10.5f };

    private float[] pos_this;
    void Start()
    {
        son = this.transform;
        switch (son.name)
        {
            case "plate_move":
                pos_this = pos1;
                break;
            case "plate_move (1)":
                pos_this = pos2;
                break;
            case "plate_move (2)":
                pos_this = pos3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ishinder && collision.transform.tag == "Player")
        {
            lose.SetActive(true);
            //暂停
            Time.timeScale = 0;
        }
    }

    private void Move()
    {
        if (son.localPosition.x <= pos_this[0] && moveToLeft)
        {
            moveToLeft = false;
        }
        else if (son.localPosition.x >= pos_this[1] && !moveToLeft)
            moveToLeft = true;
        son.localPosition += (moveToLeft ? Vector3.left : Vector3.right) * Time.deltaTime * speed;
    }
}
