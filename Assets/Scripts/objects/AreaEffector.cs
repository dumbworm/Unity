using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffector : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator ani;

    void Start()
    {
        ani = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerControl _script = collision.GetComponent<PlayerControl>();
            AreaEffector2D ae= GetComponent<AreaEffector2D>();
           int di = _script.dirc;
            if((ani.GetBool("IsN")&&di==2) ||
                (ani.GetBool("IsS") && di == 0))
            {
                ae.enabled = true;
            }
            else
            {
                ae.enabled = false;
            }

        }
    }
}
