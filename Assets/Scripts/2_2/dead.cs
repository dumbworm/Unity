﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lose;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            lose.SetActive(true);
            //暂停
            //Time.timeScale = 0;
        }
    }
}
