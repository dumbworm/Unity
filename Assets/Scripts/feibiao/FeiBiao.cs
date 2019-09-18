using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeiBiao : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lose;
    private float speed = 1f;
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.transform.tag == "Player")
        {
            lose.SetActive(true);
            //暂停
            Time.timeScale = 0;
        }
    }
    private void MoveLeft()
    {
        if (transform.localPosition.x > -13f)
        {
            transform.localPosition += Vector3.left * Time.deltaTime * speed;

        }
        else
        {
            transform.localPosition= new Vector3(13f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
