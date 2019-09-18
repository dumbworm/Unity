using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mega_key : MonoBehaviour
{
    public GameObject Door_open, Door_close;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Door_open.SetActive(true);
            Door_close.SetActive(false);
            Destroy(this.gameObject);
        }        
    }


}
