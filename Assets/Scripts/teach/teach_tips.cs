using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teach_tips : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject teach_ui;
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            close_tips();
        }



    }
    void close_tips()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }


}
