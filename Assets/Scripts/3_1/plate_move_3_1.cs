using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate_move_3_1 : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform son;
    private bool moveToLeft = true;
    private bool moveToDown = true;
    private bool isUpdown = true;
    private float speed = 2;
    private float[] pos1 = { -4.3f,-1f };
    private float[] pos2 = { -2.2f, 2.2f };
    //private float[] pos3 = { 4.68f, 10.5f };

    private float[] pos_this;
    void Start()
    {
        son = this.transform;
        switch (son.name)
        {
            case "plate":
                pos_this = pos1;
                isUpdown = true;
                break;

            case "plate (2)":
                isUpdown = true;
                pos_this = pos2;
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
        if (collision.transform.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }


    private void Move()
    {
        if (isUpdown)
        {
            if (son.localPosition.y <= pos_this[0] && moveToDown)
            {
                moveToDown = false;
            }
            else if (son.localPosition.y >= pos_this[1] && !moveToDown)
                moveToDown = true;

            son.localPosition += (moveToDown ? Vector3.down : Vector3.up) * Time.deltaTime * speed;
        }
        else
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
}
