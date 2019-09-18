using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_body : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Door_open, Door_close;
    [HideInInspector] public bool TriggerIsPlayer;
    void Start()
    {
        TriggerIsPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Key")
        {//碰到钥匙
            Door_open.SetActive(true);
            Door_close.SetActive(false);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Player")
        {//如果碰到的是玩家，则让自己的水平移动速度下降,移动速度在playcontrol代码里
            TriggerIsPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.transform.tag == "Player")
        {//如果碰到的是玩家，则让自己的水平移动速度下降,移动速度在playcontrol代码里
    
            TriggerIsPlayer = false;
        }
    }
    */
}
