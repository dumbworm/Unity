using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate_move3_3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lose;
    private Transform son;
    public bool ishinder;//是否是碰撞即销毁的障碍
    private bool moveToDown = true;
    private bool moveToLeft = true;
    public bool isUpDown = true;//是上下移动还是左右移动
    private float speed = 2f;
    private float[] pos1 = { 3.08f,6.62f };
    private float[] pos2 = { 3.08f, 6.62f };
    private float[] pos3 = { 4.47f, 10.57f };

    private float[] pos_this;
    void Start()
    {
        son = this.transform;
        switch (son.name)
        {
            case "plate_move":
                isUpDown = true;
                pos_this = pos1;
                break;
            case "plate_move (1)":
                isUpDown = true;
                pos_this = pos2;
                break;
            case "plate_move (2)":
                isUpDown = false;
                pos_this = pos3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isUpDown)
            MoveUpDown();
        else
            MoveLeftRight();
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

    private void MoveUpDown()
    {
        if (son.localPosition.y <= pos_this[0] && moveToDown)
        {
            moveToDown = false;
        }
        else if (son.localPosition.y >= pos_this[1] && !moveToDown)
            moveToDown = true;
        son.localPosition += (moveToDown ? Vector3.down : Vector3.up) * Time.deltaTime * speed;
    }
    private void MoveLeftRight()
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
